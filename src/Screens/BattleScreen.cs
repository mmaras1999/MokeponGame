using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MokeponGame.Gameplay;
using MokeponGame.UI;
using MokeponGame.UI.ImageEffects;
using MokeponGame.Managers;
using MokeponGame.UI.CanvasPresets.Battle;

namespace MokeponGame.Screens
{
    public enum BattleState { START,
                              ENEMY_CHOICE,
                              PLAYER_CHOICE,
                              CHOOSE_ACTION,
                              USE_ABILITIES,
                              END_TURN,
                              LOST,
                              WON,
                              NAME_MOKEPON
                              }

    public class BattleScreen : IScreen
    {
        Action<GameTime>[] battleMethods;

        public Enemy enemy;
        Player player;
        Mokepon playerMokepon;
        Mokepon enemyMokepon;

        Image background;
        Text battleText;
        Image playerMokeponImage;
        Image enemyMokeponImage;
        public DialogueBox Dialogues;

        MokeponBattleStats playerStats;
        MokeponBattleStats enemyStats;

        Canvas currentCanvas;

        double wait;
        bool closing;
        bool hideCanvas;

        public string PlayerMove, EnemyMove;
        Ability playerAbility, enemyAbility;
        public bool MoveChosen;
        public bool EnemyMokeponCaught;
        
        BattleState currentState;

        public BattleScreen(Enemy enemy)
        {
            this.enemy = enemy;
            player = Managers.GameManager.Instance.PlayerData;
            background = new Image("Battle/GreenBattleBackground", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight));
            battleText = new Text("BATTLE!", Color.Black, new Vector2(100, 60), Vector2.One, "Expression-pro-48px");
            Dialogues = new DialogueBox("", true, false);

            currentState = BattleState.START;
            wait = 0;
            closing = false;
            MoveChosen = false;
            hideCanvas = false;
            EnemyMokeponCaught = false;

            battleMethods = new Action<GameTime>[9]
            {
                StartBattle,
                ChooseEnemy,
                ChoosePlayer,
                ChooseAction,
                UseAbilities,
                EndTurn,
                Lost,
                Won,
                NameMokepon
            };
        }

        public void LoadContent()
        {
            background.LoadContent();
            battleText.LoadContent();
            Dialogues.LoadContent();

            if (enemy.TauntText != null)
                Dialogues.AddText(enemy.TauntText);
            Dialogues.DisplayNext();
        }

        public void UnloadContent()
        {
            background.UnloadContent();
            battleText.UnloadContent();
            Dialogues.UnloadContent();

            if (playerMokeponImage != null)
                playerMokeponImage.UnloadContent();
            if (enemyMokeponImage != null)
                enemyMokeponImage.UnloadContent();
            if (enemyStats != null)
                enemyStats.UnloadContent();
            if (playerStats != null)
                playerStats.UnloadContent();
            if (currentCanvas != null)
                currentCanvas.UnloadContent();
            GC.Collect();
        }

        public void Update(GameTime gameTime)
        {
            Dialogues.Update(gameTime);

            if (playerMokeponImage != null)
                playerMokeponImage.Update(gameTime);

            if (enemyMokeponImage != null)
                enemyMokeponImage.Update(gameTime);

            if (playerStats != null)
                playerStats.Update(gameTime);

            if (enemyStats != null)
                enemyStats.Update(gameTime);

            if (Managers.InputManager.Instance.KeysPressed(Keys.Enter))
            {
                if (Dialogues.WrappedText.Count > 0 || Dialogues.Displaying)
                {
                    Dialogues.DisplayNext();
                    return;
                }
                else if (wait > 0)
                {
                    wait = 0;
                    return;
                }
            }

            if (Dialogues.Displaying)
                return;

            if(wait > 0)
            {
                wait = Math.Max(0, wait - gameTime.ElapsedGameTime.TotalSeconds);
                return;
            }

            battleMethods[(int)currentState].DynamicInvoke(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            battleText.Draw(spriteBatch);

            if (playerMokeponImage != null)
                playerMokeponImage.Draw(spriteBatch);

            if (enemyMokeponImage != null)
                enemyMokeponImage.Draw(spriteBatch);

            if (enemyStats != null)
                enemyStats.Draw(spriteBatch);
            if (playerStats != null)
                playerStats.Draw(spriteBatch);

            Dialogues.Draw(spriteBatch);

            if (currentCanvas != null && !hideCanvas)
                currentCanvas.Draw(spriteBatch);
        }

        void StartBattle(GameTime gameTime)
        {
            if (Dialogues.WrappedText.Count == 0 && !Dialogues.Displaying)
            {
                SwitchState(BattleState.ENEMY_CHOICE);
            }
        }

        void ChooseEnemy(GameTime gameTime)
        {
            if(enemyMokepon == null)
            {
                if (enemy.Mokepons.Count == 0)
                {
                    SwitchState(BattleState.WON);
                    return;
                }
                
                foreach (var mok in enemy.Mokepons)
                {
                    if(mok.HP > 0)
                    {
                        enemyMokepon = mok;
                        break;
                    }
                }

                Dialogues.AddText(string.Format(enemy.MokeponChoiceText, enemyMokepon.Name));
                Dialogues.DisplayNext();

                enemyMokeponImage = new Image("Mokepons/" + enemyMokepon.Name);
                enemyMokeponImage.LoadContent();
                enemyMokeponImage.MoveMid(new Vector2(3 * Globals.ScreenWidth / 4, Globals.ScreenHeight / 4));
                PulseEffect pulse = new PulseEffect(2f, 1.5);
                enemyMokeponImage.AddEffect("PulseEffect", ref pulse);
                enemyMokeponImage.ActivateEffect("PulseEffect");
                
                enemyStats = new MokeponBattleStats(ref enemyMokepon, new Vector2(450, 100));
                enemyStats.LoadContent();
                wait = 3.0;
            }

            SwitchState(BattleState.PLAYER_CHOICE);
        }

        void ChoosePlayer(GameTime gameTime)
        {
            if(playerMokepon == null)
            {
                if(currentCanvas == null)
                {
                    bool lost = true;
                    foreach (var pok in player.Mokepons)
                    {
                        if (pok.HP > 0)
                            lost = false;
                    }

                    if (lost)
                    {
                        currentState = BattleState.LOST;
                        return;
                    }

                    currentCanvas = new PlayerChooseMokeponCanvas(player.Mokepons);
                    currentCanvas.LoadContent();
                }

                currentCanvas.Update(gameTime);

                if ((currentCanvas as PlayerChooseMokeponCanvas).MokeponChosen == true)
                {
                    playerMokepon = player.Mokepons[(currentCanvas as PlayerChooseMokeponCanvas).Choice];

                    currentCanvas.UnloadContent();
                    currentCanvas = null;
                    GC.Collect();

                    Dialogues.AddText(string.Format("Go! {0}!", playerMokepon.Name));
                    Dialogues.DisplayNext();

                    playerMokeponImage = new Image("Mokepons/" + playerMokepon.DefaultName);
                    playerMokeponImage.LoadContent();
                    playerMokeponImage.MoveMid(new Vector2(Globals.ScreenWidth / 4, 3 * Globals.ScreenHeight / 4 - 100));
                    PulseEffect pulse = new PulseEffect(2f, 1.5);
                    playerMokeponImage.AddEffect("PulseEffect", ref pulse);
                    playerMokeponImage.ActivateEffect("PulseEffect");

                    playerStats = new MokeponBattleStats(ref playerMokepon, new Vector2(500, 400), true);
                    playerStats.LoadContent();
                    wait = 3.0;
                    SwitchState(BattleState.CHOOSE_ACTION);
                    return;
                }
            }
            else
            {
                SwitchState(BattleState.CHOOSE_ACTION);
                return;
            }
        }

        void ChooseAction(GameTime gameTime)
        {
            if (!MoveChosen)
            {
                if (currentCanvas == null)
                {
                    Dialogues.AddText("What will " + playerMokepon.Name + " do?");
                    Dialogues.DisplayNext();
                    currentCanvas = new PlayerActionChoiceCanvas(player, playerMokepon);
                    currentCanvas.LoadContent();
                }

                currentCanvas.Update(gameTime);
            }
            else
            {
                if(currentCanvas.GetType() == typeof(PlayerActionChoiceCanvas))
                {
                    currentCanvas.UnloadContent();
                    currentCanvas = null;
                    GC.Collect();
                }

                if (PlayerMove == "Run")
                {
                    foreach(var pok in player.Mokepons)
                    {
                        pok.HP = 0;
                    }

                    Dialogues.AddText(player.Name + " has fled from the battle!");
                    Dialogues.AddText(player.Name + "\'s Mokepons have fainted!");
                    Dialogues.DisplayNext();
                    wait = 3.0;
                    SwitchState(BattleState.LOST);
                    return;
                }
                else if (PlayerMove == "SwitchMokepon")
                {
                    if (currentCanvas == null)
                    {
                        currentCanvas = new PlayerChooseMokeponCanvas(player.Mokepons, playerMokepon);
                        currentCanvas.LoadContent();
                    }

                    if ((currentCanvas as PlayerChooseMokeponCanvas).MokeponChosen == false)
                        currentCanvas.Update(gameTime);

                    if ((currentCanvas as PlayerChooseMokeponCanvas).MokeponChosen == true)
                    {
                        if(playerMokepon != null)
                        {
                            hideCanvas = true;
                            Dialogues.AddText(playerMokepon.Name + ", go back!");
                            Dialogues.DisplayNext();
                            playerMokepon = null;
                            playerMokeponImage.UnloadContent();
                            playerMokeponImage = null;
                            playerStats.UnloadContent();
                            playerStats = null;
                            GC.Collect();
                            wait = 3.0;
                            return;
                        }

                        playerMokepon = player.Mokepons[(currentCanvas as PlayerChooseMokeponCanvas).Choice];
                        currentCanvas.UnloadContent();
                        currentCanvas = null;
                        hideCanvas = false;
                        GC.Collect();

                        playerMokeponImage = new Image("Mokepons/" + playerMokepon.DefaultName);
                        playerMokeponImage.LoadContent();
                        playerMokeponImage.MoveMid(new Vector2(Globals.ScreenWidth / 4, 3 * Globals.ScreenHeight / 4 - 100));
                        PulseEffect pulse = new PulseEffect(2f, 1.5);
                        playerMokeponImage.AddEffect("PulseEffect", ref pulse);
                        playerMokeponImage.ActivateEffect("PulseEffect");

                        playerStats = new MokeponBattleStats(ref playerMokepon, new Vector2(500, 400), true);
                        playerStats.LoadContent();

                        Dialogues.AddText(string.Format("Go! {0}!", playerMokepon.Name));
                        Dialogues.DisplayNext();
                        wait = 3.0;
                        EnemyMove = EnemyAI.ChooseMove(enemyMokepon, playerMokepon, enemy.AILevel);
                        enemyAbility = (Ability)Activator.CreateInstance(Type.GetType("MokeponGame.Gameplay.Abilities." + EnemyMove));
                        SwitchState(BattleState.USE_ABILITIES);
                        return;
                    }
                }
                else if (PlayerMove == "Wait")
                {
                    Dialogues.AddText(playerMokepon.Name + " decides to wait!");
                    Dialogues.DisplayNext();
                    wait = 3.0;
                    EnemyMove = EnemyAI.ChooseMove(enemyMokepon, playerMokepon, enemy.AILevel);
                    enemyAbility = (Ability)Activator.CreateInstance(Type.GetType("MokeponGame.Gameplay.Abilities." + EnemyMove));
                    SwitchState(BattleState.USE_ABILITIES);
                    return;
                }
                else if(PlayerMove.StartsWith("Item_"))
                {
                    string[] strings = PlayerMove.Split('_');
                    EnemyMove = EnemyAI.ChooseMove(enemyMokepon, playerMokepon, enemy.AILevel);
                    enemyAbility = (Ability)Activator.CreateInstance(Type.GetType("MokeponGame.Gameplay.Abilities." + EnemyMove));
                    playerAbility = (Ability)Activator.CreateInstance(Type.GetType("MokeponGame.Gameplay.Abilities." + strings[2]));
                    playerAbility.UsedWithItem = true;
                    playerAbility.ItemName = strings[1];
                    SwitchState(BattleState.USE_ABILITIES);
                    return;
                }
                else
                {
                    EnemyMove = EnemyAI.ChooseMove(enemyMokepon, playerMokepon, enemy.AILevel);
                    enemyAbility = (Ability)Activator.CreateInstance(Type.GetType("MokeponGame.Gameplay.Abilities." + EnemyMove.Replace(" ", "")));
                    playerAbility = (Ability)Activator.CreateInstance(Type.GetType("MokeponGame.Gameplay.Abilities." + PlayerMove.Replace(" ", "")));
                    SwitchState(BattleState.USE_ABILITIES);
                    return;
                }
            }
        }

        void UseAbilities(GameTime gameTime)
        {
            //USE ABILITIES
            if (playerAbility == null && enemyAbility == null)
            {
                SwitchState(BattleState.END_TURN);
                return;
            }
            else if (enemyAbility != null && ((playerAbility == null) ||
                    (playerAbility.Priority < enemyAbility.Priority) ||
                    (playerAbility.Priority == enemyAbility.Priority && playerMokepon.SPD < enemyMokepon.SPD)))
            {
                //Use enemy ability
                if (enemyAbility.UsedWithItem)
                {
                    Dialogues.AddText(enemy.Name + " uses " + enemyAbility.ItemName + "!");
                    if (!Dialogues.Displaying)
                        Dialogues.DisplayNext();
                }
                else
                {
                    Dialogues.AddText(enemyMokepon.Name + " uses " + enemyAbility.Name + "!");
                    if (!Dialogues.Displaying)
                        Dialogues.DisplayNext();
                }

                double hit = Managers.GameManager.Instance.random.NextDouble();

                if(hit >= (enemyAbility.UsedWithItem ? (enemyAbility.ACC / 100.0) : 
                    (enemyAbility.ACC / 100.0 * enemyMokepon.ACC / 100.0)))
                {
                    Dialogues.AddText("It missed!");
                    enemyAbility = null;
                    return;
                }
                
                if (enemyAbility.Attack)
                {
                    Damage damage = GameManager.Instance.CalculateDamage(enemyMokepon, playerMokepon, enemyAbility);

                    if (damage.Critical)
                    {
                        Dialogues.AddText("Critical hit!");
                        if (!Dialogues.Displaying)
                            Dialogues.DisplayNext();
                    }

                    if (damage.Multipiler > 1)
                    {
                        Dialogues.AddText("It's super effective!");
                        if (!Dialogues.Displaying)
                            Dialogues.DisplayNext();
                    }
                    else if (damage.Multipiler < 1)
                    {
                        Dialogues.AddText("It's not very effective!");
                        if (!Dialogues.Displaying)
                            Dialogues.DisplayNext();
                    }

                    playerMokepon.ReceiveDamage(damage.DamageValue);
                }

                enemyAbility.Effect(ref enemyMokepon, ref playerMokepon, this);
                wait = 3.0;
                enemyAbility = null;
                GC.Collect();
            }
            else
            {
                //Use player ability
                if (playerAbility.UsedWithItem)
                {
                    Dialogues.AddText(player.Name + " uses " + playerAbility.ItemName + "!");
                    if (!Dialogues.Displaying)
                        Dialogues.DisplayNext();
                }
                else
                {
                    Dialogues.AddText(playerMokepon.Name + " uses " + playerAbility.Name + "!");
                    if (!Dialogues.Displaying)
                        Dialogues.DisplayNext();
                }

                double hit = Managers.GameManager.Instance.random.NextDouble();

                if (hit >= (playerAbility.UsedWithItem ? (playerAbility.ACC / 100.0) :
                    (playerAbility.ACC / 100.0 * playerMokepon.ACC / 100.0)))
                {
                    Dialogues.AddText("It missed!");
                    playerAbility = null;
                    return;
                }
         
                if (playerAbility.Attack)
                {
                    Damage damage = GameManager.Instance.CalculateDamage(playerMokepon, enemyMokepon, playerAbility);

                    if (damage.Critical)
                    {
                        Dialogues.AddText("Critical hit!");
                        if (!Dialogues.Displaying)
                            Dialogues.DisplayNext();
                    }

                    if (damage.Multipiler > 1)
                    {
                        Dialogues.AddText("It's super effective!");
                        if (!Dialogues.Displaying)
                            Dialogues.DisplayNext();
                    }
                    else if (damage.Multipiler < 1)
                    {
                        Dialogues.AddText("It's not very effective!");
                        if (!Dialogues.Displaying)
                            Dialogues.DisplayNext();
                    }

                    enemyMokepon.ReceiveDamage(damage.DamageValue);
                }

                playerAbility.Effect(ref playerMokepon, ref enemyMokepon, this);
                wait = 3.0;
                playerAbility = null;
                GC.Collect();

                if (EnemyMokeponCaught)
                {
                    Dialogues.AddText(player.Name + " has caught " + enemyMokepon.Name + "!");

                    if (player.Mokepons.Count < 6)
                    {
                        player.Mokepons.Add(enemyMokepon);

                        enemy.Mokepons.Remove(enemyMokepon);
                        enemyMokepon = null;
                        enemyStats.UnloadContent();
                        enemyMokeponImage.UnloadContent();
                        enemyStats = null;
                        enemyMokeponImage = null;
                        GC.Collect();
                        SwitchState(BattleState.NAME_MOKEPON);
                        return;
                    }
                    else
                    {
                        Dialogues.AddText(player.Name + " has reached maximum number of Mokepons!");
                        enemyMokepon.HP = 0;
                    }

                    playerAbility = null;
                    enemyAbility = null;
                }
            }

            if (playerMokepon.HP == 0)
            {
                Dialogues.AddText(playerMokepon.Name + " fainted!");
                if (!Dialogues.Displaying)
                    Dialogues.DisplayNext();
                playerMokepon = null;
                playerStats.UnloadContent();
                playerMokeponImage.UnloadContent();
                playerStats = null;
                playerMokeponImage = null;
                GC.Collect();
                SwitchState(BattleState.END_TURN);
            }

            if (enemyMokepon.HP == 0)
            {
                int XP = GameManager.Instance.CalculateExperience(playerMokepon, enemyMokepon, enemy.Name.Length > 0);
                playerMokepon.GainXP(XP);
                Dialogues.AddText(playerMokepon.Name + " gained " + XP.ToString() + " experience points!");
                if (!Dialogues.Displaying)
                    Dialogues.DisplayNext();

                while (playerMokepon.XP >= playerMokepon.LVL_UP_XP)
                {
                    playerMokepon.LevelUp();
                    Dialogues.AddText(playerMokepon.Name + " grew to level " + playerMokepon.LVL + "!");
                }

                Dialogues.AddText(enemyMokepon.Name + " fainted!");
                
                enemyMokepon = null;
                enemyStats.UnloadContent();
                enemyMokeponImage.UnloadContent();
                enemyStats = null;
                enemyMokeponImage = null;

                GC.Collect();
                SwitchState(BattleState.END_TURN);
            }
        }

        void Won(GameTime gameTime)
        {
            if(currentCanvas != null)
            {
                currentCanvas.UnloadContent();
                currentCanvas = null;
                GC.Collect();
            }

            if (!closing)
            {
                closing = true;
                Dialogues.AddText(player.Name + " has won the battle!");
                int money = (int)Math.Round(GameManager.Instance.random.NextDouble() * (enemy.AILevel * enemy.AILevel + 1) * 100);
                Dialogues.AddText(player.Name + " gained " + money.ToString() + " Respect Points!");
                player.Money += money;
                Dialogues.DisplayNext();
                wait = 3.0;
            }
            else
            {
                EraseBattleBuffs();
                Managers.ScreenManager.Instance.ChangeScreen("GameScreen");
            }
        }

        void Lost(GameTime gameTime)
        {
            if (currentCanvas != null)
            {
                currentCanvas.UnloadContent();
                currentCanvas = null;
                GC.Collect();
            }

            if (!closing)
            {
                closing = true;
                Dialogues.AddText(player.Name + " has no Mokepon left!");
                Dialogues.AddText(player.Name + " has lost the battle!");
                int money = (int)Math.Round(GameManager.Instance.random.NextDouble() * (enemy.AILevel * enemy.AILevel + 1) * 100);
                int prevMoney = player.Money;
                player.Money = Math.Max(0, player.Money - money);
                Dialogues.AddText(player.Name + " lost " + (prevMoney - player.Money).ToString() + " Respect Points!");
                Dialogues.DisplayNext();
                wait = 3.0;
            }
            else
            {
                EraseBattleBuffs();
                Managers.ScreenManager.Instance.ChangeScreen("GameScreen");
            }
        }

        void EndTurn(GameTime gameTime)
        {
            //CHECK IF LOST
            bool lost = true;
            foreach (var pok in player.Mokepons)
            {
                if (pok.HP > 0)
                    lost = false;
            }

            if(lost)
            {
                currentState = BattleState.LOST;
                return;
            }

            //CHECK IF WON
            bool won = true;
            foreach (var pok in enemy.Mokepons)
            {
                if (pok.HP > 0)
                    won = false;
            }

            if(won)
            {
                currentState = BattleState.WON;
                return;
            }

            //CONTINUE
            currentState = BattleState.ENEMY_CHOICE;
        }

        void NameMokepon(GameTime gameTime)
        {
            if (currentCanvas == null)
            {
                currentCanvas = new NameCapturedMokeponCanvas();
                currentCanvas.LoadContent();
            }

            if (!(currentCanvas as NameCapturedMokeponCanvas).Finished)
            {
                currentCanvas.Update(gameTime);
            }
            else
            {
                currentCanvas.UnloadContent();
                currentCanvas = null;
                GC.Collect();
                Dialogues.AddText(player.Mokepons.Last().Name + " now belongs to " + player.Name + "!");
                SwitchState(BattleState.WON);
                return;
            }
        }

        public void EraseBattleBuffs()
        {
            foreach (var mok in player.Mokepons)
            {
                mok.EraseBattleBuffs();
            }
        }

        void SwitchState(BattleState newState)
        {
            MoveChosen = false;
            if(currentCanvas != null)
            {
                currentCanvas.UnloadContent();
                currentCanvas = null;
                GC.Collect();
            }

            currentState = newState;
        }
    }
}

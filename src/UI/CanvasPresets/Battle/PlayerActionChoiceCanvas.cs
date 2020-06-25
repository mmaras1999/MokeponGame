using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MokeponGame.Gameplay;

namespace MokeponGame.UI.CanvasPresets.Battle
{
    public class PlayerActionChoiceCanvas : Canvas
    {
        public enum Mode {MAIN, ATTACK, ITEM};
        Player player;
        Mokepon playerMokepon;
        Screens.BattleScreen screen;
        Mode currentMode;

        public PlayerActionChoiceCanvas(Player player, Mokepon playerMokepon)
        {
            this.player = player;
            this.playerMokepon = playerMokepon;
            screen = (Screens.BattleScreen)Managers.ScreenManager.Instance.CurrentScreen;
            SetMode(Mode.MAIN);
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(currentMode == Mode.ATTACK || currentMode == Mode.ITEM)
            {
                if(Managers.InputManager.Instance.KeysPressed(Keys.Escape))
                {
                    SetMode(Mode.MAIN);
                    return;
                }
            }
        }

        public override void ButtonOnClick(int buttonid)
        {
            base.ButtonOnClick(buttonid);

            if (currentMode == Mode.MAIN)
            {
                if (buttonid == 0)
                {
                    SetMode(Mode.ATTACK);
                    return;
                }
                else if (buttonid == 1)
                {
                    int moks = 0;

                    foreach (var mok in player.Mokepons)
                    {
                        if (mok.HP > 0)
                            ++moks;
                    }

                    if (moks <= 1)
                    {
                        screen.Dialogues.AddText(player.Name + " has no other Mokepons!");
                        screen.Dialogues.DisplayNext();
                        return;
                    }
                    else
                    {
                        screen.MoveChosen = true;
                        screen.PlayerMove = "SwitchMokepon";
                        return;
                    }
                }
                else if(buttonid == 2)
                {
                    SetMode(Mode.ITEM);
                    return;
                }
                else if (buttonid == 3)
                {
                    screen.MoveChosen = true;
                    screen.PlayerMove = "Wait";
                    return;
                }
                else if (buttonid == 4)
                {
                    screen.MoveChosen = true;
                    screen.PlayerMove = "Run";
                    return;
                }
            }
            else if(currentMode == Mode.ATTACK)
            {
                screen.MoveChosen = true;
                screen.PlayerMove = Buttons[buttonid].ButtonText.TextValue.Replace(" ", "");
                return;
            }
            else if(currentMode == Mode.ITEM)
            {
                screen.MoveChosen = true;
                screen.PlayerMove = player.Items[buttonid].AbilityName.Replace(" ", "");
                player.DeleteItem(player.Items[buttonid]);
                return;
            }
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            if(previous != current)
            {
                Buttons[previous].ButtonText.Color = Color.White;
                Buttons[current].ButtonText.Color = Color.Gray;
            }

            base.SwitchButtonSelection(previous, current);
        }

        public void SetMode(Mode mode)
        {
            if(mode == Mode.MAIN)
            {
                foreach (var button in Buttons)
                {
                    button.UnloadContent();
                }
                Buttons.Clear();

                currentMode = mode;
                ButtonRows = 1;
                CurrentButton = 0;

                Buttons.Add(new Button(null, new Text("Attack", Color.Gray, Vector2.Zero, Vector2.One, "Expression-pro-32px")));
                Buttons.Add(new Button(null, new Text("Switch Mokepon", Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px")));
                Buttons.Add(new Button(null, new Text("Item", Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px")));
                Buttons.Add(new Button(null, new Text("Wait", Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px")));
                Buttons.Add(new Button(null, new Text("Run", Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px")));
                LoadContent();

                Buttons[0].Move(new Vector2(50, Globals.ScreenHeight - 50 - 32));
                Buttons[1].Move(new Vector2(250, Globals.ScreenHeight - 50 - 32));
                Buttons[2].Move(new Vector2(650, Globals.ScreenHeight - 50 - 32));
                Buttons[3].Move(new Vector2(850, Globals.ScreenHeight - 50 - 32));
                Buttons[4].Move(new Vector2(1050, Globals.ScreenHeight - 50 - 32));
            }
            else if (mode == Mode.ATTACK)
            {
                if (playerMokepon.AbilityNames.Count == 0)
                {
                    screen.Dialogues.AddText(playerMokepon.Name + " has no abilities!");
                    screen.Dialogues.DisplayNext();
                    return;
                }

                foreach (var button in Buttons)
                {
                    button.UnloadContent();
                }
                Buttons.Clear();

                currentMode = mode;
                ButtonRows = 1;
                CurrentButton = 0;

                foreach (var name in playerMokepon.AbilityNames)
                {
                    Buttons.Add(new Button(null, new Text(name, Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px")));
                }

                Buttons[0].ButtonText.Color = Color.Gray;

                LoadContent();

                float prevX = 50;

                foreach (var button in Buttons)
                {
                    button.Move(new Vector2(prevX, Globals.ScreenHeight - 50 - 32));
                    prevX += 50 + button.ButtonText.GetSize().X;
                }
            }
            else if(mode == Mode.ITEM)
            {
                if(player.Items.Count == 0)
                {
                    screen.Dialogues.AddText(player.Name + " has nothing in his inventory!");
                    screen.Dialogues.DisplayNext();
                    return;
                }

                foreach (var but in Buttons)
                {
                    but.UnloadContent();
                }
                Buttons.Clear();

                currentMode = mode;
                ButtonRows = 1;
                CurrentButton = 0;

                foreach(var item in player.Items)
                {
                    Buttons.Add(new Button(null, new Text(item.Name, Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px")));
                }

                Buttons[0].ButtonText.Color = Color.Gray;
                LoadContent();

                float prevX = 50;

                foreach (var button in Buttons)
                {
                    button.Move(new Vector2(prevX, Globals.ScreenHeight - 50 - 32));
                    prevX += 50 + button.ButtonText.GetSize().X;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using MokeponGame.Managers;
using MokeponGame.Gameplay;
using MokeponGame.UI;
using MokeponGame.UI.CanvasPresets;

namespace MokeponGame.Screens
{
    class GameScreen : IScreen
    {
        public bool Paused;

        ContentManager content;

        //Sprites
        Canvas currentCanvas;
        Image playerImage;
        Text locationName;

        //Movement
        double movementSpeed;
        double movementWait;
        Vector2[] movementVectors;
        Vector2 translation;

        Player player;
        GameWorld world;

        public GameScreen()
        {
            Paused = false;
            currentCanvas = null;
            movementSpeed = 0.25;
            movementWait = 0.0;
            translation = Vector2.Zero;

            movementVectors = new Vector2[4]
                {
                    new Vector2(0, 1),
                    new Vector2(-1, 0),
                    new Vector2(1, 0),
                    new Vector2(0, -1)
                };

            if (GameManager.Instance.PlayerData == null)
            {
                GameManager.Instance.PlayerData = XmlManager.Instance.Load<Player>("Data/save" + 
                    GameManager.Instance.GameSaveID.ToString() + ".xml");
            }

            player = GameManager.Instance.PlayerData;

            GameManager.Instance.LoadWorld(player.Location);
            world = GameManager.Instance.World;

            playerImage = new Image("Character/" + player.BodyType, new Rectangle(32, 0, 32, 32), 
                                    Vector2.Zero, 64, 64);
            playerImage.AddEffect("AnimationSheetEffect");
            locationName = new Text(world.Name, "Expression-pro-32px");
            locationName.Color = Color.White;
        }

        public void LoadContent()
        {
            content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            playerImage.LoadContent();
            playerImage.MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2));

            world.LoadContent();
            locationName.LoadContent();
        }

        public void UnloadContent()
        {
            world.UnloadContent();
            playerImage.UnloadContent();

            if (locationName != null)
                locationName.UnloadContent();

            content.Unload();
            GameManager.Instance.SaveGame();
            GC.Collect();
        }

        public void Update(GameTime gameTime)
        {
            if (locationName != null)
                UpdateLocationName(gameTime);

            if (Paused && currentCanvas != null)
            {
                currentCanvas.Update(gameTime);
            }
            else if (!Paused)
            {
                if (InputManager.Instance.KeysPressed(Keys.Escape))
                {
                    ChangeCanvas("EscapeMenu");
                    return;
                }

                if(InputManager.Instance.KeysPressed(Keys.Enter))
                {
                    int nextX = player.PositionX + (int)movementVectors[player.Direction].X;
                    int nextY = player.PositionY + (int)movementVectors[player.Direction].Y;

                    world.TriggerActions(nextX, nextY);
                    return;
                }

                if(InputManager.Instance.KeysPressed(Keys.Tab))
                {
                    ChangeCanvas("TabMenu");
                    return;
                }

                if (movementWait > 0)
                {
                    movementWait -= gameTime.ElapsedGameTime.TotalSeconds;
                    translation -= (float)(gameTime.ElapsedGameTime.TotalSeconds / movementSpeed) * movementVectors[player.Direction];
                    playerImage.ActivateEffect("AnimationSheetEffect");

                    if (movementWait <= 0)
                    {
                        playerImage.DeactivateEffect("AnimationSheetEffect");
                        translation = Vector2.Zero;
                        player.PositionX += (int)(movementVectors[player.Direction].X);
                        player.PositionY += (int)(movementVectors[player.Direction].Y);

                        world.TriggerStepped(player.PositionX, player.PositionY);
                    }
                }

                if (movementWait <= 0)
                {
                    CheckMovement();
                }

                world.Update(gameTime);
                playerImage.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            world.Draw(spriteBatch, player.PositionX, player.PositionY, translation);
            playerImage.Draw(spriteBatch);

            if(currentCanvas != null)
            {
                currentCanvas.Draw(spriteBatch);
            }

            if (locationName != null)
                locationName.Draw(spriteBatch);
        }

        //CHECK IF PLAYER MOVED CHARACTER
        public void CheckMovement()
        {
            if (InputManager.Instance.KeysDown(Keys.Left))
            {
                player.Direction = 1;

                if (world.Passable(player.PositionX - 1, player.PositionY))
                {
                    movementWait = movementSpeed;
                }
            }
            else if (InputManager.Instance.KeysDown(Keys.Right))
            {
                player.Direction = 2;

                if (world.Passable(player.PositionX + 1, player.PositionY))
                { 
                    movementWait = movementSpeed;
                }
            }
            else if (InputManager.Instance.KeysDown(Keys.Up))
            {
                player.Direction = 3;

                if (world.Passable(player.PositionX, player.PositionY - 1))
                {
                    movementWait = movementSpeed;
                }
            }
            else if (InputManager.Instance.KeysDown(Keys.Down))
            {
                player.Direction = 0;

                if (world.Passable(player.PositionX, player.PositionY + 1))
                {
                    movementWait = movementSpeed;
                }
            }

            playerImage.SourceRect.Y = 32 * player.Direction;
        }

        public void ChangeCanvas(string canvasName)
        {
            Canvas newCanvas = null;

            if(currentCanvas != null)
            {
                currentCanvas.UnloadContent();
            }

            Paused = false;
            
            if(canvasName != "null")
            {
                Paused = true;
                newCanvas = (Canvas)Activator.CreateInstance(Type.GetType("MokeponGame.UI.CanvasPresets." + canvasName + "Canvas"));
                newCanvas.LoadContent();
            }

            currentCanvas = newCanvas;
        }

        public void ChangeCanvas(Canvas newCanvas)
        {
            if (currentCanvas != null)
            {
                currentCanvas.UnloadContent();
            }

            Paused = false;

            if (newCanvas != null)
            {
                Paused = true;
                newCanvas.LoadContent();
            }

            currentCanvas = newCanvas;
        }

        public void UpdateLocationName(GameTime gameTime)
        {
            locationName.MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 3));
            locationName.Alpha = Math.Max(0, locationName.Alpha - (float)gameTime.ElapsedGameTime.TotalSeconds);

            if (locationName.Alpha == 0)
                locationName = null;
        }

        public void DisplayDialogue(string dialogue)
        {
            Canvas newCanvas = new DialogueCanvas(dialogue);
            if (currentCanvas != null)
                currentCanvas.UnloadContent();

            Paused = true;

            newCanvas.LoadContent();
            currentCanvas = newCanvas;
        }

        public void StartBattle(Enemy enemy)
        {
            BattleScreen battleScreen = new BattleScreen(enemy);
            Managers.ScreenManager.Instance.ChangeScreen(battleScreen);
        }

        public void ChangeMap(string map, int d, int x, int y)
        {
            player.PositionX = x;
            player.PositionY = y;
            player.Direction = d;
            player.Location = map;

            world.UnloadContent();
            GameManager.Instance.LoadWorld(player.Location);
            world = GameManager.Instance.World;
            world.LoadContent();
            locationName = new Text(world.Name, "Expression-pro-32px");
            locationName.Color = Color.White;
            locationName.LoadContent();
            GameManager.Instance.SaveGame();
        }
    }
}

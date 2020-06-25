using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MokeponGame.Managers;

namespace MokeponGame.UI.CanvasPresets
{
    public class SelectAvatarCanvas : Canvas
    {
        public SelectAvatarCanvas() : base()
        {
            GameManager.Instance.PlayerData.BodyType = 1;
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            Elements.Add(new Text("Select your appearance:", "Expression-pro-32px"));
            Elements.Add(new Image("Character/" + GameManager.Instance.PlayerData.BodyType.ToString(),
                                   new Rectangle(32, 0, 32, 32), Vector2.Zero, 96, 96));
            ButtonRows = 1;
            Text buttonText = new Text("Select", "Expression-pro-32px");
            buttonText.Color = Color.DimGray;
            Buttons.Add(new Button(null, buttonText));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));
            Elements[2].MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2));
            Buttons[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, 550));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeysPressed(Keys.Escape))
            {
                (ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("CharacterName");
                return;
            }

            if(InputManager.Instance.KeysPressed(Keys.Left))
            {
                GameManager.Instance.PlayerData.BodyType--;

                if (GameManager.Instance.PlayerData.BodyType == 0)
                    GameManager.Instance.PlayerData.BodyType = Globals.CharacterTypeCount;

                Elements[2].UnloadContent();
                Elements.RemoveAt(2);
                Elements.Add(new Image("Character/" + GameManager.Instance.PlayerData.BodyType.ToString(),
                                   new Rectangle(32, 0, 32, 32), Vector2.Zero, 96, 96));
                Elements[2].LoadContent();
                Elements[2].MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2));
                GC.Collect();
            }

            if(InputManager.Instance.KeysPressed(Keys.Right))
            {
                GameManager.Instance.PlayerData.BodyType++;

                if (GameManager.Instance.PlayerData.BodyType > Globals.CharacterTypeCount)
                    GameManager.Instance.PlayerData.BodyType = 1;

                Elements[2].UnloadContent();
                Elements.RemoveAt(2);
                Elements.Add(new Image("Character/" + GameManager.Instance.PlayerData.BodyType.ToString(),
                                   new Rectangle(32, 0, 32, 32), Vector2.Zero, 96, 96));
                Elements[2].LoadContent();
                Elements[2].MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2));
                GC.Collect();
            }
        }

        public override void ButtonOnClick(int buttonid)
        {
            if(buttonid == 0)
            {
                GC.Collect();
                (ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("Introduction");
                return;
            }

            base.ButtonOnClick(buttonid);
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            base.SwitchButtonSelection(previous, current);
        }
    }
}

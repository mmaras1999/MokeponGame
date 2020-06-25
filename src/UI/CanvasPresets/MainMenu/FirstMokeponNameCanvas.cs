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
    public class MokeponNameCanvas : Canvas
    {
        InsertStringCanvas insertName;

        public MokeponNameCanvas() : base()
        {
            insertName = new InsertStringCanvas(GameManager.Instance.PlayerData.Mokepons[0].Name);
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            Elements.Add(new Text("Select Mokepon Name:", "Expression-pro-32px"));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));
            insertName.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeysPressed(Keys.Escape))
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("SelectMokepon");
                return;
            }

            if(insertName.Inserted)
            {
                GameManager.Instance.PlayerData.Mokepons[0].Name = insertName.Input;
                GameManager.Instance.SaveGame();
                ScreenManager.Instance.ChangeScreen("GameScreen");
                return;
            }

            insertName.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            insertName.Draw(spriteBatch);
        }

        public override void ButtonOnClick(int buttonid)
        {
            base.ButtonOnClick(buttonid);
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            base.SwitchButtonSelection(previous, current);
        }
    }
}

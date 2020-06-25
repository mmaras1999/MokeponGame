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
    public class CharacterNameCanvas : Canvas
    {
        InsertStringCanvas insertCanvas;

        public CharacterNameCanvas() : base()
        {
            GameManager.Instance.PlayerData.Name = "";
            insertCanvas = new InsertStringCanvas(GameManager.Instance.PlayerData.Name);
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            Elements.Add(new Text("Select Character Name:", "Expression-pro-32px"));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));
            insertCanvas.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.Instance.KeysPressed(Keys.Escape))
            {
                GameManager.Instance.PlayerData = null;
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("SelectSave");
                return;
            }

            if (insertCanvas.Inserted)
            {
                GameManager.Instance.PlayerData.Name = insertCanvas.Input;
                (ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("SelectAvatar");
                return;
            }

            insertCanvas.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            insertCanvas.Draw(spriteBatch);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MokeponGame.UI.CanvasPresets
{
    public class DialogueCanvas : Canvas
    {
        DialogueBox box;

        public DialogueCanvas(string text) : base()
        {
            box = new DialogueBox(text);
        }

        public override void LoadContent()
        {
            base.LoadContent();
            box.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Managers.InputManager.Instance.KeysPressed(Keys.Enter))
            {
                box.DisplayNext();
            }

            if (!box.Ended)
            {
                box.Update(gameTime);
            }
            else
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas("null");
                return;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (!box.Ended)
                box.Draw(spriteBatch);
        }
    }
}

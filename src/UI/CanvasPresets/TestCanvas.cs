using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MokeponGame.UI
{
    public class TestCanvas : Canvas
    {
        DialogueBox box;
        bool started = false;

        public TestCanvas() : base()
        {
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            box = new DialogueBox("To jest jakis bardzo dlugi dialog, ktory z pewnoscia nie powinien sie zmiescic w tym polu, a zwlaszcza w jednej linijce. Sprawdzmy, czy nasz niesamowity system dialogowy dziala tak jak powinien. Zobaczmy co sie stanie po wyswietleniu okienka...");
                    
        }

        public override void LoadContent()
        {
            base.LoadContent();
            box.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(!started && Managers.InputManager.Instance.KeysPressed(Keys.Enter))
            {
                box.DisplayNext();
            }

            if (!box.Ended)
            {
                box.Update(gameTime);
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

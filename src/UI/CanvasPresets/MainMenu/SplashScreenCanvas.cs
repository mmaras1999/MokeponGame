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
    public class SplashScreenCanvas : Canvas
    {
        double wait = 2.0;
        double fadeSpeed = 0.5;

        public SplashScreenCanvas()
        {
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            Elements.Add(new Image("SplashScreen", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(wait > 0)
                wait -= gameTime.ElapsedGameTime.TotalSeconds;

            if (wait <= 0)
            {
                (Elements[1] as Image).Alpha -= (float)(fadeSpeed * gameTime.ElapsedGameTime.TotalSeconds);

                if ((Elements[1] as Image).Alpha <= 0 || Managers.InputManager.Instance.AnyKeysPressed(Keys.Escape, Keys.Enter, Keys.Space))
                {
                    (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("MainMenu");
                }
            }
        }
    }
}

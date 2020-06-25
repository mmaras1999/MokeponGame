using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using MokeponGame.UI;
using MokeponGame.UI.CanvasPresets;

namespace MokeponGame.Screens
{
    class MainMenuScreen : IScreen
    {
        private ContentManager content;
        private Canvas canvas;

        public MainMenuScreen()
        {
            canvas = new SplashScreenCanvas();
            canvas.Active = true;
            canvas.Visible = true;
        }

        public void LoadContent()
        {
            content = new ContentManager(Managers.ScreenManager.Instance.Content.ServiceProvider, "Content");
            canvas.LoadContent();
        }

        public void UnloadContent()
        {
            canvas.UnloadContent();
            content.Unload();
            GC.Collect();
        }

        public void Update(GameTime gameTime)
        {
            if (canvas != null)
            {
                canvas.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (canvas != null)
                canvas.Draw(spriteBatch);
        }

        //CHANGE CURRENT CANVAS
        public void ChangeCanvas(string canvasName)
        {
            Canvas newCanvas = null;

            if(canvasName != "null")
            {
                newCanvas = (Canvas)Activator.CreateInstance(Type.GetType("MokeponGame.UI.CanvasPresets." + canvasName + "Canvas"));
                newCanvas.LoadContent();
            }

            canvas.UnloadContent();
            GC.Collect();
            canvas = newCanvas;
        }
    }
}


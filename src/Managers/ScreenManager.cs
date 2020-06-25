using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MokeponGame.Screens;

namespace MokeponGame.Managers
{
    internal class ScreenManager
    {
        private static ScreenManager instance;
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }
        
        public ContentManager Content { private set; get; }
        public IScreen CurrentScreen; //Current Screen
        public bool Exit; //if program should exit

        private ScreenManager()
        {
            CurrentScreen = new MainMenuScreen();
        }

        public void LoadContent(ContentManager content)
        {
            Content = new ContentManager(content.ServiceProvider, "Content");
            CurrentScreen.LoadContent();
        }

        public void UnloadContent()
        {
            CurrentScreen.UnloadContent();
            GC.Collect();
        }

        public void Update(GameTime gameTime)
        {
            CurrentScreen.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentScreen.Draw(spriteBatch);
        }

        //CHANGES CURRENT SCREEN
        public void ChangeScreen(string name)
        {
            CurrentScreen.UnloadContent();

            if(name == "Exit")
            {
                Exit = true;
                return;
            }

            IScreen newScreen = (IScreen)Activator.CreateInstance(Type.GetType("MokeponGame.Screens." + name));
            newScreen.LoadContent();
            CurrentScreen = newScreen;
        }
    
        public void ChangeScreen(IScreen screen)
        {
            CurrentScreen.UnloadContent();

            screen.LoadContent();
            CurrentScreen = screen;
        }
    }
}

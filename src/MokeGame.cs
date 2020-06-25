using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MokeponGame
{
    public class MokeGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MokeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        //GAME INITIALIZATION
        protected override void Initialize()
        {
            //SET UP WINDOW SIZE AND GRAPHICS OPTIONS
            Window.AllowUserResizing = false;
            graphics.PreferredBackBufferWidth = Globals.ScreenWidth;
            graphics.PreferredBackBufferHeight = Globals.ScreenHeight;
            graphics.PreferMultiSampling = false;
            graphics.ApplyChanges();

            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            base.Initialize();
        }

        //LOAD CONTENT (IMAGES, FONTS, ETC.)
        protected override void LoadContent()
        {
            //Draws textures
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            Managers.ScreenManager.Instance.LoadContent(Content);
        }

        //UNLOAD CONTENT
        protected override void UnloadContent()
        {
            Managers.ScreenManager.Instance.UnloadContent();
        }

        //GAME LOOP -- UPDATE GAME LOGIC
        protected override void Update(GameTime gameTime)
        {
            Managers.ScreenManager.Instance.Update(gameTime);
            Managers.InputManager.Instance.Update(gameTime);

            if (Managers.ScreenManager.Instance.Exit)
                this.Exit();

            base.Update(gameTime);
        }

        //DRAW SPRITES ON SCREEN
        protected override void Draw(GameTime gameTime)
        {
            //REFRESH SCREEN
            GraphicsDevice.Clear(Color.Black);
            
            //DRAW SPRITES
            spriteBatch.Begin(SpriteSortMode.Deferred, null,SamplerState.PointClamp);
            Managers.ScreenManager.Instance.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

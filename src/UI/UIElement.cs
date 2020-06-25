using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MokeponGame.UI
{
    public abstract class UIElement
    {
        public Vector2 Position, Scale;
        protected ContentManager content;

        public UIElement()
        {
            Position = Vector2.Zero;
            Scale = Vector2.One;
        }

        public UIElement(Vector2 position, Vector2 scale)
        {
            Position = position;
            Scale = scale;
        }

        public virtual void LoadContent()
        {
            content = new ContentManager(Managers.ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void MoveVector(Vector2 position)
        {

        }

        public virtual void Move(Vector2 position)
        {

        }

        public virtual void MoveMid(Vector2 position)
        {

        }
    }
}

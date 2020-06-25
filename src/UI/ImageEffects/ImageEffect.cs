using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MokeponGame.UI
{
    public class ImageEffect
    {
        protected Image image;
        public bool Active;

        public ImageEffect()
        {
            Active = false;
        }

        public virtual void LoadContent(ref Image image)
        {
            this.image = image;
        }

        public virtual void UnloadContent()
        {
        
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }
    }
}

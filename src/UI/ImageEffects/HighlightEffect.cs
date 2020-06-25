using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MokeponGame.UI.ImageEffects
{
    public class HighlightEffect : ImageEffect
    {
        Color defaultColor;
        Color color;

        public HighlightEffect()
        {
            color = Color.Black;
        }

        public HighlightEffect(Color color)
        {
            this.color = color;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
            defaultColor = image.ImageColor;
        }

        public override void UnloadContent()
        {
            image.ImageColor = defaultColor;
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Active)
                image.ImageColor = color;
            else
                image.ImageColor = defaultColor;
        }
    }
}

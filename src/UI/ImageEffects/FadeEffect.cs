using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MokeponGame.UI.ImageEffects
{
    public class FadeEffect : ImageEffect
    {
        float effectSpeed;

        public FadeEffect(float speed = 1.0f)
        {
            effectSpeed = speed;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Active && image.Alpha > 0)
            {
                image.Alpha = Math.Max(0, image.Alpha - effectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if(image.Alpha < image.MaxAlpha)
            {
                image.Alpha = Math.Min(image.MaxAlpha, image.Alpha + effectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}

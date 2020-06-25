using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MokeponGame.UI.ImageEffects
{
    public class AppearEffect : ImageEffect
    {
        float effectSpeed;
        float Alpha = 1.0f;

        public AppearEffect(float alpha = 1.0f, float speed = 1.0f)
        {
            Alpha = alpha;
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

            if (Active && image.Alpha < Alpha)
            {
                image.Alpha = Math.Min(Alpha, image.Alpha + effectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (!Active && image.Alpha > image.MaxAlpha)
            {
                image.Alpha = Math.Max(image.MaxAlpha, image.Alpha - effectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}

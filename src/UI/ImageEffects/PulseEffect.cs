using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MokeponGame.UI.ImageEffects
{
    public class PulseEffect : ImageEffect
    {
        float effectSpeed;
        double duration;
        bool decrease;

        public PulseEffect()
        {
            effectSpeed = 1.0f;
            duration = -1;
            decrease = false;
        }

        public PulseEffect(float speed)
        {
            effectSpeed = speed;
            decrease = false;
            duration = -1;
        }

        public PulseEffect(float speed, double duration)
        {
            effectSpeed = speed;
            decrease = false;
            this.duration = duration;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            image.Alpha = image.MaxAlpha;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (duration >= 0)
            {
                duration = Math.Max(0, duration - gameTime.ElapsedGameTime.TotalSeconds);

                if(duration == 0)
                {
                    Active = false;
                    image.Alpha = image.MaxAlpha;
                }
            }

            if (image.Alpha == image.MaxAlpha)
                decrease = true;
            if (image.Alpha == 0)
                decrease = false;

            if (Active && decrease)
            {
                image.Alpha = Math.Max(0, image.Alpha - effectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                image.Alpha = Math.Min(image.MaxAlpha, image.Alpha + effectSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }
    }
}

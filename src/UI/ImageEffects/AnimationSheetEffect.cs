using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MokeponGame.UI.ImageEffects
{
    class AnimationSheetEffect : ImageEffect
    {
        List<int> changes;
        double delay;
        double toWait;
        int position;
        int defaultPos;

        public AnimationSheetEffect() : base()
        {
            changes = new List<int>() { -1, 1, 1, -1};
            delay = 0.1;
            toWait = delay;
            position = 0;
        }

        public override void LoadContent(ref Image image)
        {
            base.LoadContent(ref image);
            defaultPos = image.SourceRect.X;
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                if (toWait > 0)
                    toWait -= gameTime.ElapsedGameTime.TotalSeconds;

                if (toWait <= 0)
                {
                    image.SourceRect.X += changes[position] * 32;
                    position = (position + 1) % 4;
                    toWait = delay;
                }
            }
            else
            {
                image.SourceRect.X = defaultPos;
                toWait = delay;
            }

            base.Update(gameTime);
        }
    }
}

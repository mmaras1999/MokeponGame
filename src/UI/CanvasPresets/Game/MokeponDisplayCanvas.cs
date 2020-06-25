using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MokeponGame.Gameplay;

namespace MokeponGame.UI.CanvasPresets
{
    public class MokeponDisplayCanvas : MokeponListCanvas
    {
        List<Mokepon> mokepons;

        public MokeponDisplayCanvas(List<Mokepon> mokepons) : base(mokepons)
        {
            this.mokepons = mokepons;
            Elements.Add(new Text("Your Mokepons:", Color.White, Vector2.Zero, Vector2.One, "Expression-pro-32px"));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements.Last().MoveMid(new Vector2(Globals.ScreenWidth / 2, 50));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Managers.InputManager.Instance.AnyKeysPressed(Keys.Escape, Keys.Tab))
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas("TabMenu");
                return;
            }
        }

        public override void ButtonOnClick(int buttonid)
        {
            base.ButtonOnClick(buttonid);

            Canvas c = new MokeponOptionsCanvas(mokepons[buttonid]);
            (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas(c);
            return;
        }
    }
}

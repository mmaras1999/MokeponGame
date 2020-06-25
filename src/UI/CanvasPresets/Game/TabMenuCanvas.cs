using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MokeponGame.UI.CanvasPresets
{
    public class TabMenuCanvas : Canvas
    {
        public TabMenuCanvas()
        {
            Elements.Add(new Image("Black", new Rectangle(Globals.ScreenWidth / 4, Globals.ScreenHeight / 4,
                                                          Globals.ScreenWidth / 2, Globals.ScreenHeight / 2), 0.5f));
            Buttons.Add(new Button(null, new Text("Mokepons", "Expression-pro-32px")));
            Buttons.Add(new Button(null, new Text("Back", "Expression-pro-32px")));
            Buttons[0].ButtonText.Color = Color.DimGray;
            Buttons[1].ButtonText.Color = Color.White;
            ButtonRows = 2;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Buttons[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 - 100));
            Buttons[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight / 2 + 100));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Managers.InputManager.Instance.AnyKeysPressed(Keys.Escape, Keys.Tab))
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas("null");
                return;
            }
        }

        public override void ButtonOnClick(int buttonid)
        {
            if (buttonid == 0)
            {
                MokeponDisplayCanvas canvas = new MokeponDisplayCanvas(Managers.GameManager.Instance.PlayerData.Mokepons);
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas(canvas);
                return;
            }
            else if (buttonid == 1)
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas("null");
                return;
            }

            base.ButtonOnClick(buttonid);
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            if (previous != current)
            {
                Buttons[previous].ButtonText.Color = Color.White;
                Buttons[current].ButtonText.Color = Color.DimGray;
            }

            base.SwitchButtonSelection(previous, current);
        }
    }
}

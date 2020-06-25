using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MokeponGame.UI.CanvasPresets
{
    public class MainMenuCanvas : Canvas
    {
        public MainMenuCanvas() : base()
        {
            CurrentButton = 0;
            ButtonRows = 3; //START, CREDITS, EXIT

            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
                
            Text startButtonText = new Text("Start Game", 
                                            Color.DimGray, 
                                            new Vector2(450, 300), 
                                            new Vector2(1, 1), 
                                            "Expression-pro-32px");

            Text creditsButtonText = new Text("Credits",
                                            Color.White,
                                            new Vector2(450, 400),
                                            new Vector2(1, 1),
                                            "Expression-pro-32px");

            Text exitButtonsText = new Text("Exit",
                                            Color.White,
                                            new Vector2(450, 500),
                                            new Vector2(1, 1),
                                            "Expression-pro-32px");

            Buttons.Add(new Button(null, startButtonText));
            Buttons.Add(new Button(null, creditsButtonText));
            Buttons.Add(new Button(null, exitButtonsText));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            for (int i = 0; i < Buttons.Count; ++i)
                Buttons[i].MoveMid(new Vector2(Globals.ScreenWidth / 2, 300 + 100 * i));
        }

        public override void ButtonOnClick(int buttonid)
        {
            if (buttonid == 0)
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("SelectSave");
            }
            else if (buttonid == 1)
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("Credits");
            }
            else if (buttonid == 2)
            {
                Managers.ScreenManager.Instance.ChangeScreen("Exit");
            }
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            if(previous != current)
            {
                Buttons[previous].ButtonText.Color = Color.White;
                Buttons[current].ButtonText.Color = Color.DimGray;
            }
            base.SwitchButtonSelection(previous, current);
        }
    }
}

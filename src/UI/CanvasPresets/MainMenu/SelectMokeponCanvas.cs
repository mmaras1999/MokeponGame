using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MokeponGame.Managers;

namespace MokeponGame.UI.CanvasPresets
{
    public class SelectMokeponCanvas : Canvas
    {
        public SelectMokeponCanvas() : base()
        {
            GameManager.Instance.PlayerData.Mokepons.Clear();
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            Elements.Add(new Text("Choose your Mokepon!", "Expression-pro-32px"));
            Elements.Add(new Image("Mokepons/Bushy"));
            Elements.Add(new Image("Mokepons/Candle"));
            Elements.Add(new Image("Mokepons/Octave"));

            ButtonRows = 1;
            Text chooseBushy = new Text("Bushy", "Expression-pro-32px");
            Text chooseCandle = new Text("Candle", "Expression-pro-32px");
            Text chooseOctave = new Text("Octave", "Expression-pro-32px");
            chooseBushy.Color = Color.DimGray;
            chooseCandle.Color = Color.White;
            chooseOctave.Color = Color.White;
            Buttons.Add(new Button(null, chooseBushy));
            Buttons.Add(new Button(null, chooseCandle));
            Buttons.Add(new Button(null, chooseOctave));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));
            Elements[2].MoveMid(new Vector2(Globals.ScreenWidth / 6, Globals.ScreenHeight / 2));
            Elements[3].MoveMid(new Vector2(Globals.ScreenWidth / 2 + 10, Globals.ScreenHeight / 2));
            Elements[4].MoveMid(new Vector2(Globals.ScreenWidth * 5 / 6, Globals.ScreenHeight / 2));

            Buttons[0].MoveMid(new Vector2(Globals.ScreenWidth / 6, 550));
            Buttons[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 550));
            Buttons[2].MoveMid(new Vector2(Globals.ScreenWidth * 5 / 6, 550));
        }

        public override void ButtonOnClick(int buttonid)
        {
            if (buttonid == 0 || buttonid == 1 || buttonid == 2)
            {
                GameManager.Instance.PlayerData.Mokepons.Add(new Gameplay.Mokepon(Buttons[buttonid].ButtonText.TextValue, 1)); 
                GC.Collect();
                (ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("MokeponName");
                return;
            }

            base.ButtonOnClick(buttonid);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MokeponGame.UI.CanvasPresets
{
    public class ContinueGameCanvas : Canvas
    {
        bool warning = false;

        public ContinueGameCanvas() : base()
        {
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            ButtonRows = 2;

            Buttons.Add(new Button(null, new Text("Continue Game", "Expression-pro-32px")));
            Buttons.Add(new Button(null, new Text("Delete Save", "Expression-pro-32px")));
            Buttons[0].ButtonText.Color = Color.DimGray;
            Buttons[1].ButtonText.Color = Color.White;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Buttons[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, 250));
            Buttons[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 400));
        }

        public override void ButtonOnClick(int buttonid)
        {
            if (buttonid == 0)
            {
                Managers.ScreenManager.Instance.ChangeScreen("GameScreen");
                return;
            }
            else if(buttonid == 1)
            {
                if(warning)
                {
                    File.Delete("Data/save" + Managers.GameManager.Instance.GameSaveID.ToString() + ".xml");
                    (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("CharacterName");
                    return;
                }
                else
                {
                    Elements.Add(new Text("WARNING! This will permanently remove your saved game!\n", "Expression-pro-32px"));
                    Elements.Last().LoadContent();
                    (Elements.Last() as Text).Color = Color.Red;
                    Elements.Last().MoveMid(new Vector2(Globals.ScreenWidth / 2, 650));
                    Managers.GameManager.Instance.PlayerData = new Gameplay.Player();
                    warning = true;
                }
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

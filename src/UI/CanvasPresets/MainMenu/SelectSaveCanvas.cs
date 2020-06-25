using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MokeponGame.Managers;

namespace MokeponGame.UI.CanvasPresets
{
    public class SelectSaveCanvas : Canvas
    {
        public SelectSaveCanvas() : base()
        {
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            Elements.Add(new Text("Select Game Save:", "Expression-pro-32px"));

            ButtonRows = 2;
            CurrentButton = 0;

            for (int i = 0; i < 4; ++i)
            {
                Text saveText;
                if (File.Exists("Data/save" + i.ToString() + ".xml"))
                {
                    Gameplay.Player p = XmlManager.Instance.Load<Gameplay.Player>("Data/save" + i.ToString() + ".xml");
                    saveText = new Text(p.Name, "Expression-pro-32px");
                    Image playerImage = new Image("Character/" + p.BodyType, new Rectangle(32, 0, 32, 32), Vector2.Zero, 64, 64);
                    Elements.Add(playerImage);
                }
                else
                {
                    saveText = new Text("Empty Save", "Expression-pro-32px");
                }

                saveText.Color = Color.White;
                Buttons.Add(new Button(null, saveText));
            }

            Buttons[0].ButtonText.Color = Color.DimGray;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));

            int cnt = 2;
            for (int i = 0; i < 4; ++i)
            {
                Buttons[i].MoveMid(new Vector2(Globals.ScreenWidth / 4, 200 + 300 * (i % 2)));

                if (i > 1)
                    Buttons[i].MoveVector(new Vector2(Globals.ScreenWidth / 2, 0));

                if(Buttons[i].ButtonText.TextValue != "Empty Save")
                {
                    Elements[cnt].MoveMid(new Vector2(Globals.ScreenWidth / 4, 280 + 300 * (i % 2)));

                    if (i > 1)
                        Elements[cnt].MoveVector(new Vector2(Globals.ScreenWidth / 2, 0));
                    ++cnt;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.KeysPressed(Keys.Escape))
            {
                (ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("MainMenu");
                return;
            }

            base.Update(gameTime);
        }

        public override void ButtonOnClick(int buttonid)
        {
            GameManager.Instance.GameSaveID = buttonid;

            if (Buttons[buttonid].ButtonText.TextValue == "Empty Save")
            {
                GameManager.Instance.PlayerData = new Gameplay.Player();
                (ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("CharacterName");
                return;
            }
            else
            {
                GameManager.Instance.PlayerData = XmlManager.Instance.Load<Gameplay.Player>("Data/save" +
                                                                                            buttonid.ToString() + ".xml");
                (ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("ContinueGame");
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

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
    public class InsertStringCanvas : Canvas
    {
        public bool Inserted;
        string keysOrder;
        Dictionary<Keys, char> textKeys;
        bool shift = false;
        bool errorShowed = false;
        public string Input;      

        public InsertStringCanvas(string to_insert) : base()
        {
            Inserted = false;
            Input = to_insert;
            Elements.Add(new Text(to_insert + "_", "Expression-pro-32px"));
            Elements.Add(new Image("Black", new Rectangle(40, 300, Globals.ScreenWidth - 80, 300), 0.3f));

            keysOrder = "qazwsxedcrfvtgbyhnujmik_ol.p\',";

            textKeys = new Dictionary<Keys, char>()
            {
                {Keys.A, 'a' },
                {Keys.B, 'b' },
                {Keys.C, 'c' },
                {Keys.D, 'd' },
                {Keys.E, 'e' },
                {Keys.F, 'f' },
                {Keys.G, 'g' },
                {Keys.H, 'h' },
                {Keys.I, 'i' },
                {Keys.J, 'j' },
                {Keys.K, 'k' },
                {Keys.L, 'l' },
                {Keys.M, 'm' },
                {Keys.N, 'n' },
                {Keys.O, 'o' },
                {Keys.P, 'p' },
                {Keys.Q, 'q' },
                {Keys.R, 'r' },
                {Keys.S, 's' },
                {Keys.T, 't' },
                {Keys.U, 'u' },
                {Keys.V, 'v' },
                {Keys.W, 'w' },
                {Keys.X, 'x' },
                {Keys.Y, 'y' },
                {Keys.Z, 'z' },
                {Keys.Space, ' ' },
                {Keys.OemComma, ',' },
                {Keys.OemPeriod, '.' },
                {Keys.OemMinus, '-' },
                {Keys.OemQuotes, '\'' }
            };

            CurrentButton = 0;
            ButtonRows = 3;

            foreach (var c in keysOrder)
            {
                Text buttonText = new Text(c + "", "Expression-pro-32px");
                Buttons.Add(new Button(null, buttonText));
                buttonText.Color = Color.White;
            }

            Text buttonShiftText = new Text("SHIFT", "Expression-pro-32px");
            buttonShiftText.Color = Color.White;
            Buttons.Add(new Button(null, buttonShiftText));
            Text buttonBackText = new Text("BACK", "Expression-pro-32px");
            buttonBackText.Color = Color.White;
            Buttons.Add(new Button(null, buttonBackText));
            Text buttonEnterText = new Text("ENTER", "Expression-pro-32px");
            buttonEnterText.Color = Color.White;
            Buttons.Add(new Button(null, buttonEnterText));
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, 250));

            for (int i = 0; i < Buttons.Count - 3; i++)
            {
                Buttons[i].MoveMid(new Vector2(150 + 75 * (i / 3), 350 + 100 * (i % 3)));
            }

            for (int i = Buttons.Count - 3; i < Buttons.Count; i++)
            {
                Buttons[i].MoveMid(new Vector2(1000, 350 + 100 * (i % 3)));
            }

            Buttons[CurrentButton].ButtonText.Color = Color.Gray;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            foreach (var k in textKeys)
            {
                if (InputManager.Instance.KeysPressed(k.Key) ||
                    (previousKey == k.Key && to_wait <= 0))
                {
                    currentKey = k.Key;
                    to_wait = delay;
                    char c = k.Value;
                    if (InputManager.Instance.AnyKeysDown(Keys.LeftShift, Keys.RightShift))
                    {
                        c = Char.ToUpper(c);
                    }

                    Input += c;
                    (Elements[0] as Text).TextValue = Input + '_';
                    Elements[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, 250));
                }
            }

            if (InputManager.Instance.KeysPressed(Keys.Back) ||
                (previousKey == Keys.Back && to_wait <= 0))
            {
                to_wait = delay;
                currentKey = Keys.Back;

                if (Input.Length > 0)
                {
                    Input = Input.Remove(Input.Length - 1);
                    (Elements[0] as Text).TextValue = Input + '_';
                    Elements[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, 250));
                }
            }
        }

        public override void ButtonOnClick(int buttonid)
        {
            if (Buttons[buttonid].ButtonText.TextValue == "SHIFT")
            {
                ToggleShift();
            }
            else if (Buttons[buttonid].ButtonText.TextValue == "BACK")
            {
                if (Input.Length > 0)
                {
                    Input = Input.Remove(Input.Length - 1);
                    (Elements[0] as Text).TextValue = Input + '_';
                    Elements[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, 250));
                }
            }
            else if (Buttons[buttonid].ButtonText.TextValue == "ENTER")
            {
                if (Input.Length == 0 || Input.Length > 12)
                {
                    if (!errorShowed)
                    {
                        Text errorText = new Text("Length should be between 1 and 12 characters!", "Expression-pro-32px");
                        errorText.Color = Color.DarkRed;
                        errorText.LoadContent();
                        errorText.MoveMid(new Vector2(Globals.ScreenWidth / 2, 650));
                        Elements.Add(errorText);
                        errorShowed = true;
                    }

                    return;
                }

                Inserted = true;
                return;
            }
            else
            {
                string c = Buttons[buttonid].ButtonText.TextValue;
                if (c == "_")
                    c = " ";
                Input += c;
                (Elements[0] as Text).TextValue = Input + '_';
                Elements[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, 250));
            }

            base.ButtonOnClick(buttonid);
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            if (previous != current)
            {
                Buttons[previous].ButtonText.Color = Color.White;
                Buttons[current].ButtonText.Color = Color.Gray;
            }

            base.SwitchButtonSelection(previous, current);
        }

        public void ToggleShift()
        {
            foreach (var button in Buttons)
            {
                if (button.ButtonText.TextValue.Length == 1)
                {
                    if (!shift)
                    {
                        button.ButtonText.TextValue = button.ButtonText.TextValue.ToUpper();
                    }
                    else
                    {
                        button.ButtonText.TextValue = button.ButtonText.TextValue.ToLower();
                    }
                }
            }

            shift = !shift;
        }
    }
}

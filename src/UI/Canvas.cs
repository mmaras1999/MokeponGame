using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MokeponGame.UI
{
    public abstract class Canvas
    {
        public List<UIElement> Elements;
        public List<Button> Buttons;
        public bool Active;
        public bool Visible;
        public int ButtonRows, CurrentButton;
        protected Keys previousKey, currentKey;
        protected double delay;
        protected double to_wait;

        public Canvas()
        {
            Active = true;
            Visible = true;
            Elements = new List<UIElement>();
            Buttons = new List<Button>();
            ButtonRows = CurrentButton = 0;
            previousKey = Keys.None;
            currentKey = Keys.None;
            delay = 0.25;
            to_wait = delay;
        }

        public virtual void LoadContent()
        {
            foreach (var elem in Elements)
                elem.LoadContent();
            foreach (var button in Buttons)
                button.LoadContent();
        }

        public virtual void UnloadContent()
        {
            foreach (var elem in Elements)
                elem.UnloadContent();
            foreach (var button in Buttons)
                button.UnloadContent();
            GC.Collect();
        }

        public virtual void Update(GameTime gameTime)
        {
            if(Active)
            {
                previousKey = currentKey;
                currentKey = Keys.None;
                foreach (var elem in Elements)
                    elem.Update(gameTime);

                foreach (var button in Buttons)
                    button.Update(gameTime);

                if (Buttons.Count > 0 && ButtonRows > 0)
                {
                    if(previousKey != Keys.None)
                    {
                        if (!Managers.InputManager.Instance.KeysDown(previousKey))
                        {
                            previousKey = Keys.None;
                            to_wait = delay;
                        }
                        else if (to_wait > 0)
                        {
                            to_wait -= gameTime.ElapsedGameTime.TotalSeconds;
                            currentKey = previousKey;
                        }
                    }

                    if (Managers.InputManager.Instance.KeysPressed(Keys.Enter) || 
                        (previousKey == Keys.Enter && to_wait <= 0))
                    {
                        if (Buttons[CurrentButton].Active)
                            ButtonOnClick(CurrentButton);
                        currentKey = Keys.Enter;
                        to_wait = delay;
                    }

                    if (Managers.InputManager.Instance.KeysPressed(Keys.Down) || 
                        (previousKey == Keys.Down && to_wait <= 0))
                    {
                        if (CurrentButton % ButtonRows < ButtonRows - 1 && CurrentButton + 1 < Buttons.Count)
                            SwitchButtonSelection(CurrentButton, CurrentButton + 1);
                        else
                            SwitchButtonSelection(CurrentButton, CurrentButton);
                        currentKey = Keys.Down;
                        to_wait = delay;
                    }

                    if (Managers.InputManager.Instance.KeysPressed(Keys.Up) || 
                        (previousKey == Keys.Up && to_wait <= 0))
                    {
                        if (CurrentButton % ButtonRows > 0 && CurrentButton - 1 >= 0)
                            SwitchButtonSelection(CurrentButton, CurrentButton - 1);
                        else
                            SwitchButtonSelection(CurrentButton, CurrentButton);
                        currentKey = Keys.Up;
                        to_wait = delay;
                    }

                    if (Managers.InputManager.Instance.KeysPressed(Keys.Right) ||
                        (previousKey == Keys.Right && to_wait <= 0))
                    {
                        if (CurrentButton + ButtonRows < Buttons.Count)
                            SwitchButtonSelection(CurrentButton, CurrentButton + ButtonRows);
                        else
                            SwitchButtonSelection(CurrentButton, CurrentButton);
                        currentKey = Keys.Right;
                        to_wait = delay;
                    }

                    if (Managers.InputManager.Instance.KeysPressed(Keys.Left) ||
                        (previousKey == Keys.Left && to_wait <= 0))
                    {
                        if (CurrentButton - ButtonRows >= 0)
                            SwitchButtonSelection(CurrentButton, CurrentButton - ButtonRows);
                        else
                            SwitchButtonSelection(CurrentButton, CurrentButton);
                        currentKey = Keys.Left;
                        to_wait = delay;
                    }
                }

            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(Visible)
            {
                foreach (var elem in Elements)
                    elem.Draw(spriteBatch);
                foreach (var button in Buttons)
                    button.Draw(spriteBatch);
            }
        }

        public virtual void ButtonOnClick(int buttonid)
        {

        }

        public virtual void SwitchButtonSelection(int previous, int current)
        {
            CurrentButton = current;
        }
    }
}

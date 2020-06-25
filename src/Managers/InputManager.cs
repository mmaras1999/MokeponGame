using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MokeponGame.Managers
{
    public class InputManager
    {
        private static InputManager instance;
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new InputManager();
                return instance;
            }
        }

        private KeyboardState currentState, prevState;

        //UPDATES KEYBOARD STATE
        public void Update(GameTime gameTime)
        {
            prevState = currentState;
            currentState = Keyboard.GetState();
        }

        //CHECKS IF ANY OF GIVEN KEYS WAS PRESSED
        public bool AnyKeysPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentState.IsKeyDown(key) && prevState.IsKeyUp(key))
                    return true;
            }

            return false;
        }

        //CHECK IF ALL GIVEN KEYS WERE PRESSED
        public bool KeysPressed(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (!(currentState.IsKeyDown(key) && prevState.IsKeyUp(key)))
                    return false;
            }

            return true;
        }

        //CHECKS IF ANY OF GIVEN KEYS IS DOWN
        public bool AnyKeysDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (currentState.IsKeyDown(key))
                    return true;
            }

            return false;
        }

        //CHECKS IF ALL GIVEN KEYS ARE DOWN
        public bool KeysDown(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (!currentState.IsKeyDown(key))
                    return false;
            }

            return true;
        }

        //CHECKS IF ANY OF GIVEN KEYS WAS RELEASED
        public bool AnyKeysReleased(params Keys[] keys)
        {
            foreach (Keys key in keys)
            {
                if (!(currentState.IsKeyUp(key) && prevState.IsKeyDown(key)))
                    return true;
            }

            return false;
        }

        //CHECKS IF ALL GIVEN KEYS WERE RELEASED
        public bool KeysReleased(params Keys[] keys)
        {
            foreach(Keys key in keys)
            {
                if (!(currentState.IsKeyUp(key) && prevState.IsKeyDown(key)))
                    return false;
            }

            return true;
        }

        //RETURNS ALL PRESSED KEYS
        public Keys[] GetPressedKeys()
        {
            return currentState.GetPressedKeys();
        }
    }
}

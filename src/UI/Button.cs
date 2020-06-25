using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MokeponGame.UI
{
    public class Button : UIElement
    {
        public Image ButtonImage;
        public Text ButtonText;
        public bool Active;

        public Button(Image image, Text text) : base()
        {
            ButtonImage = image;
            ButtonText = text;
            Active = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            if (ButtonImage != null)
                ButtonImage.LoadContent();
            if (ButtonText != null)
                ButtonText.LoadContent();
        }

        public override void UnloadContent()
        {
            if (ButtonImage != null)
                ButtonImage.UnloadContent();
            if (ButtonText != null)
                ButtonText.UnloadContent();
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (ButtonImage != null)
                ButtonImage.Update(gameTime);
            if (ButtonText != null)
                ButtonText.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (ButtonImage != null)
                ButtonImage.Draw(spriteBatch);
            if (ButtonText != null)
                ButtonText.Draw(spriteBatch);
        }

        public override void Move(Vector2 position)
        {
            if (ButtonImage != null)
                ButtonImage.Move(position);
            if (ButtonText != null)
                ButtonText.Move(position);
        }

        public override void MoveMid(Vector2 position)
        {
            if (ButtonImage != null)
                ButtonImage.MoveMid(position);
            if (ButtonText != null)
                ButtonText.MoveMid(position);
        }

        public override void MoveVector(Vector2 position)
        {
            if (ButtonImage != null)
                ButtonImage.MoveVector(position);
            if (ButtonText != null)
                ButtonText.MoveVector(position);
        }
    }
}

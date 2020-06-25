using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace MokeponGame.UI
{
    public class Text : UIElement
    {
        public string TextValue, FontName;
        public Color Color;
        public float Alpha;
        public SpriteFont Sfont;

        public Text(string text = "", string fontname = "Expression-pro-32px", float alpha = 1.0f) : base()
        {
            TextValue = text;
            FontName = fontname;
            Color = Color.Black;
            Alpha = alpha;
        }

        public Text(string text, Color color, Vector2 position, Vector2 scale, 
                    string fontname = "Expression-pro-32px", float alpha = 1.0f) : base(position, scale)
        {
            TextValue = text;
            Color = color;
            Scale = scale;
            FontName = fontname;
            Alpha = alpha;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Sfont = content.Load<SpriteFont>("Fonts/" + FontName);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Sfont, TextValue, Position, Color * Alpha);
        }

        public override void MoveVector(Vector2 position)
        {
            Position += position;
        }

        public override void Move(Vector2 position)
        {
            Position = position;
        }

        public override void MoveMid(Vector2 position)
        {
            Vector2 size = Sfont.MeasureString(TextValue);
            Position = new Vector2(position.X - size.X / 2, position.Y - size.Y / 2);
        }

        public Vector2 GetSize()
        {
            return Sfont.MeasureString(TextValue);
        }
    }
}

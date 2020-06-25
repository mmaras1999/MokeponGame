using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MokeponGame.Gameplay;

namespace MokeponGame.UI
{
    public class ExpBar : UIElement
    {
        public int Height;
        public int Width;
        Mokepon pokemon;
        Image back;
        Image bar;

        public ExpBar(ref Mokepon pok, int height, int width, Vector2 pos)
        {
            pokemon = pok;
            Width = width;
            Height = height;
            Position = pos;
            Scale = Vector2.One;
            back = new Image("Black", new Rectangle(0, 0, 1, 1), pos, width, height);
            bar = new Image("White", new Rectangle(0, 0, 1, 1), pos + new Vector2(1, 1), width - 2, height - 2);
            bar.ImageColor = Color.LightSkyBlue;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            back.LoadContent();
            bar.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            back.UnloadContent();
            bar.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            bar.Scale = new Vector2((float)pokemon.XP / pokemon.LVL_UP_XP, 1);
            bar.Update(gameTime);
            back.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            back.Draw(spriteBatch);
            bar.Draw(spriteBatch);
        }

        public override void Move(Vector2 position)
        {
            base.Move(position);
            back.Move(position);
            bar.Move(position + new Vector2(1, 1));
        }

        public override void MoveMid(Vector2 position)
        {
            throw new NotImplementedException();
        }

        public override void MoveVector(Vector2 position)
        {
            Position += position;
            back.MoveVector(position);
            bar.MoveVector(position);
        }
    }
}

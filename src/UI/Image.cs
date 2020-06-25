using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MokeponGame.UI
{
    public class Image : UIElement
    {
        public float Alpha;
        public float MaxAlpha;
        public string Path;
        public int Width;
        public int Height;
        public Color ImageColor;
        public Texture2D Texture;
        public Dictionary<string, ImageEffect> effects;
        public Rectangle SourceRect;

        public Image(string path = "") : base()
        {
            Path = path;
            Alpha = 1.0f;
            MaxAlpha = Alpha;
            effects = new Dictionary<string, ImageEffect>();
            ImageColor = Color.White;
            Width = -1;
            Height = -1;
            SourceRect = Rectangle.Empty;
        }

        //Since Vector2 argument cannot have default value we have to overload constructor 
        public Image(string path, Vector2 position, Vector2 scale, float alpha = 1.0f) : base(position, scale)
        {
            Path = path;
            Alpha = alpha;
            MaxAlpha = Alpha;
            effects = new Dictionary<string, ImageEffect>();
            ImageColor = Color.White;
            Width = -1;
            Height = -1;
            SourceRect = Rectangle.Empty;
        }

        public Image(string path, Rectangle destRect, float alpha = 1.0f)
        {
            Path = path;
            Alpha = alpha;
            MaxAlpha = Alpha;
            ImageColor = Color.White;
            effects = new Dictionary<string, ImageEffect>();
            Position = new Vector2(destRect.X, destRect.Y);
            Width = destRect.Width;
            Height = destRect.Height;
            SourceRect = Rectangle.Empty;
        }

        public Image(string path, Rectangle sourceRect, Vector2 position, float alpha = 1.0f)
        {
            Path = path;
            Alpha = MaxAlpha = alpha;
            effects = new Dictionary<string, ImageEffect>();
            Position = position;
            ImageColor = Color.White;
            Width = sourceRect.Width;
            Height = sourceRect.Height;
            SourceRect = sourceRect;
        }

        public Image(string path, Rectangle sourceRect, Vector2 position, int width, int height, float alpha = 1.0f)
        {
            Path = path;
            Alpha = MaxAlpha = alpha;
            Position = position;
            effects = new Dictionary<string, ImageEffect>();
            ImageColor = Color.White;
            Width = width;
            Height = height;
            SourceRect = sourceRect;
        }

        //Add effect type
        public void AddEffect(string effectType)
        {
            var obj = this;
            ImageEffect effect = (ImageEffect)Activator.CreateInstance(Type.GetType("MokeponGame.UI.ImageEffects." + effectType));
            effect.LoadContent(ref obj);
            effects.Add(effectType, effect);
        }

        //Some effects may have some constructors with arguments. In that case we can just pass reference to already created object
        public void AddEffect<T>(string effectType, ref T effect)
        {
            if (effect == null)
                return;
            var obj = this;
            (effect as ImageEffect).LoadContent(ref obj);
            effects.Add(effectType, (effect as ImageEffect));
        }

        public bool EffectActive(string effectType)
        {
            if (effects.ContainsKey(effectType))
                return effects[effectType].Active;

            return false;
        }

        public void ActivateEffect(string effectType)
        {
            if(effects.ContainsKey(effectType))
            {
                var obj = this;
                effects[effectType].LoadContent(ref obj);
                effects[effectType].Active = true;
            }
        }

        public void DeactivateEffect(string effectType)
        {
            if(effects.ContainsKey(effectType))
            {
                effects[effectType].Active = false;
                effects[effectType].UnloadContent();
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            if (Path.Length > 0)
            {
                Texture = content.Load<Texture2D>(Path);

                if (Width == -1)
                    Width = Texture.Width;
                if (Height == -1)
                    Height = Texture.Height;
                if (SourceRect == Rectangle.Empty)
                    SourceRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
            }
        }

        public override void UnloadContent()
        {   
            foreach (var effect in effects)
            {
                effect.Value.Active = false;
                effect.Value.UnloadContent();
            }
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var effect in effects)
            {
                effect.Value.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, new Rectangle((int)Position.X, (int)Position.Y, (int)(Width * Scale.X), (int)(Height * Scale.Y)), 
                             SourceRect, ImageColor * Alpha);
        }

        public override void Move(Vector2 position)
        {
            Position = position;
        }

        public override void MoveMid(Vector2 position)
        {
            if(Texture != null)
            {
                float w = Width * Scale.X;
                float h = Height * Scale.Y;
                Position = new Vector2(position.X - w / 2, position.Y - h / 2);
            }
        }

        public override void MoveVector(Vector2 position)
        {
            Position += position;
        }
    }
}

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
    public class MokeponOverview : Button
    {
        public int Width, Height;
        public Image Background;
        Mokepon pokemon;
        Image pokemonImage;

        Text pokemonName, pokemonLevel;
        Text hpStatus;
        Text[] pokemonStats;
        HPBar hpbar;
        ExpBar expbar;

        public MokeponOverview(Mokepon pokemon, Vector2 pos) : base(null, null)
        {
            Position = pos;
            Scale = Vector2.One;
            Width = 400;
            Height = 200;
            this.pokemon = pokemon;

            Background = new Image("White", new Rectangle((int)pos.X, (int)pos.Y, Width, Height), 0.8f);
            pokemonImage = new Image("Mokepons/" + pokemon.DefaultName);

            pokemonName = new Text(pokemon.Name, Color.Black, pos + new Vector2(150, 14), Vector2.One, "Expression-pro-18px");
            pokemonLevel = new Text("Lvl " + pokemon.LVL.ToString(), Color.Black, pos + new Vector2(310, 15), Vector2.One, "Expression-pro-18px");
            hpbar = new HPBar(ref pokemon, 6, 150, pos + new Vector2(150, 55));
            hpStatus = new Text("", Color.Black, pos + new Vector2(310, 55), Vector2.One, "Expression-pro-18px");
            expbar = new ExpBar(ref pokemon, 4, 120, pos + new Vector2(150, 60));

            pokemonStats = new Text[6];

            pokemonStats[0] = new Text("ATK: " + pokemon.ATK, Color.Black, 
                                    pos + new Vector2(150, 100), Vector2.One, "Expression-pro-18px");
            pokemonStats[1] = new Text("DEF: " + pokemon.DEF, Color.Black,
                                    pos + new Vector2(275, 100), Vector2.One, "Expression-pro-18px");
            pokemonStats[2] = new Text("SP ATK: " + pokemon.SP_ATK, Color.Black,
                                    pos + new Vector2(150, 125), Vector2.One, "Expression-pro-18px");
            pokemonStats[3] = new Text("SP DEF: " + pokemon.SP_DEF, Color.Black,
                                    pos + new Vector2(275, 125), Vector2.One, "Expression-pro-18px");
            pokemonStats[4] = new Text("SPD: " + pokemon.SPD, Color.Black,
                                    pos + new Vector2(150, 150), Vector2.One, "Expression-pro-18px");
            pokemonStats[5] = new Text("ACC: " + pokemon.ACC, Color.Black,
                                    pos + new Vector2(275, 150), Vector2.One, "Expression-pro-18px");
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Background.LoadContent();
            pokemonImage.LoadContent();

            int maxSize = Math.Max(pokemonImage.Width, pokemonImage.Height);

            if(maxSize > 100)
            {
                pokemonImage.Scale = Vector2.One * 100.0f / maxSize;
            }

            pokemonImage.MoveMid(Position + new Vector2(75, 100));

            pokemonName.LoadContent();
            pokemonLevel.LoadContent();
            hpbar.LoadContent();
            hpStatus.LoadContent();
            expbar.LoadContent();

            for (int i = 0; i < 6; ++i)
                pokemonStats[i].LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Background.UnloadContent();
            pokemonImage.UnloadContent();
            pokemonName.UnloadContent();
            pokemonLevel.UnloadContent();
            hpbar.UnloadContent();           
            hpStatus.UnloadContent();
            expbar.UnloadContent();

            for (int i = 0; i < 6; ++i)
                pokemonStats[i].UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Background.Update(gameTime);
            pokemonImage.Update(gameTime);
            pokemonName.Update(gameTime);
            pokemonLevel.Update(gameTime);
            hpbar.Update(gameTime);
            hpStatus.TextValue = pokemon.HP.ToString() + " / " + pokemon.MaxHP.ToString();
            hpStatus.Update(gameTime);
            expbar.Update(gameTime);

            for (int i = 0; i < 6; ++i)
                pokemonStats[i].Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            Background.Draw(spriteBatch);
            pokemonImage.Draw(spriteBatch);
            pokemonName.Draw(spriteBatch);
            pokemonLevel.Draw(spriteBatch);
            hpbar.Draw(spriteBatch);
            hpStatus.Draw(spriteBatch);
            expbar.Draw(spriteBatch);


            for (int i = 0; i < 6; ++i)
                pokemonStats[i].Draw(spriteBatch);
        }

        public override void Move(Vector2 position)
        {
            Vector2 mv = position - Position;
            Position = position;

            Background.MoveVector(mv);
            pokemonImage.MoveVector(mv);
            pokemonName.MoveVector(mv);
            pokemonLevel.MoveVector(mv);
            hpbar.MoveVector(mv);
            hpStatus.MoveVector(mv);
            expbar.MoveVector(mv);

            for (int i = 0; i < 6; ++i)
                pokemonStats[i].MoveVector(mv);
        }

        public override void MoveMid(Vector2 position)
        {
            int midX = Width / 2;
            int midY = Height / 2;

            Vector2 mv = position - Position - new Vector2(midX, midY);

            MoveVector(mv);
        }

        public override void MoveVector(Vector2 position)
        {
            Position += position;
            Background.MoveVector(position);
            pokemonImage.MoveVector(position);
            pokemonName.MoveVector(position);
            pokemonLevel.MoveVector(position);
            hpbar.MoveVector(position);
            hpStatus.MoveVector(position);
            expbar.MoveVector(position);
            for (int i = 0; i < 6; ++i)
                pokemonStats[i].MoveVector(position);
        }
    }
}

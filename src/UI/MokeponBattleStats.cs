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
    public class MokeponBattleStats : UIElement
    {
        bool mine;
        Mokepon pokemon;
        Image background;
        Text pokemonName, pokemonLevel;
        Text hpStatus;
        HPBar hpbar;
        ExpBar expbar;

        public MokeponBattleStats(ref Mokepon pokemon, Vector2 pos, bool mine = false)
        {
            Position = pos;
            Scale = Vector2.One;

            this.mine = mine;
            this.pokemon = pokemon;
            
            background = new Image(mine ? "MyMokeponStatus" : "MokeponStatus", pos, Vector2.One);
            pokemonName = new Text(pokemon.Name, Color.Black, pos + new Vector2(10, 10), Vector2.One, "Expression-pro-24px");
            pokemonLevel = new Text("Lvl " + pokemon.LVL.ToString(), Color.Black, pos + new Vector2(200, 10), Vector2.One, "Expression-pro-18px");
            pokemonName.MoveVector(mine ? new Vector2(18, 0) : new Vector2(0, 0));
            pokemonLevel.MoveVector(mine ? new Vector2(18, 0) : new Vector2(0, 0));
            hpbar = new HPBar(ref pokemon, 6, 150, pos + new Vector2(10, 55));
            hpbar.MoveVector(mine ? new Vector2(18, 0) : new Vector2(0, 0));

            if (mine)
            {
                hpStatus = new Text("", Color.Black, pos + new Vector2(190, 55), Vector2.One, "Expression-pro-18px");
                expbar = new ExpBar(ref pokemon, 4, 120, pos + new Vector2(28, 60));
            }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            background.LoadContent();
            pokemonName.LoadContent();
            pokemonLevel.LoadContent();
            hpbar.LoadContent();

            if (mine)
            {
                hpStatus.LoadContent();
                expbar.LoadContent();
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            background.UnloadContent();
            pokemonName.UnloadContent();
            pokemonLevel.UnloadContent();
            hpbar.UnloadContent();

            if (mine)
            {
                hpStatus.UnloadContent();
                expbar.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            background.Update(gameTime);
            pokemonName.Update(gameTime);
            pokemonLevel.TextValue = "Lvl " + pokemon.LVL.ToString();
            pokemonLevel.Update(gameTime);
            hpbar.Update(gameTime);

            if (mine)
            {
                hpStatus.TextValue = pokemon.HP.ToString() + " / " + pokemon.MaxHP.ToString();
                hpStatus.Update(gameTime);
                expbar.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            background.Draw(spriteBatch);
            pokemonName.Draw(spriteBatch);
            pokemonLevel.Draw(spriteBatch);
            hpbar.Draw(spriteBatch);

            if (mine)
            {
                hpStatus.Draw(spriteBatch);
                expbar.Draw(spriteBatch);
            }
        }

        public override void Move(Vector2 position)
        {
            Vector2 mv = position - Position;
            Position = position;

            background.MoveVector(mv);
            pokemonName.MoveVector(mv);
            pokemonLevel.MoveVector(mv);
            hpbar.MoveVector(mv);
            hpStatus.MoveVector(mv);
            expbar.MoveVector(mv);
        }

        public override void MoveMid(Vector2 position)
        {
            throw new NotImplementedException();    
        }

        public override void MoveVector(Vector2 position)
        {
            Position += position;
            background.MoveVector(position);
            pokemonName.MoveVector(position);
            pokemonLevel.MoveVector(position);
            hpbar.MoveVector(position);
            hpStatus.MoveVector(position);
            expbar.MoveVector(position);
        }
    }
}

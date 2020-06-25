using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MokeponGame.Gameplay;
using MokeponGame.UI.ImageEffects;

namespace MokeponGame.UI.CanvasPresets
{
    public class MokeponListCanvas : Canvas
    {
        protected List<Mokepon> poks;

        public MokeponListCanvas(List<Mokepon> pokemons)
        {
            poks = pokemons;
            if (pokemons.Count > 6)
                throw new ApplicationException("Player has more than 6 Mokepons!");

            if (pokemons.Count == 0)
                throw new ApplicationException("Player has no Mokemons!");

            Elements.Add(new Image("BlueBackground"));
            ButtonRows = 2;
            
            foreach (var pok in pokemons)
            {
                Buttons.Add(new MokeponOverview(pok, Vector2.Zero));
                HighlightEffect he = new HighlightEffect(Color.LightGray);
                (Buttons.Last() as MokeponOverview).Background.AddEffect("HighlightEffect", ref he);
            }

            for(int i = pokemons.Count; i < 6; ++i)
            {
                Elements.Add(new Image("White", new Rectangle(20 + (i / 2) * 420, 180 + (i % 2) * 220, 400, 200), 0.8f));
            }

            (Buttons[0] as MokeponOverview).Background.ActivateEffect("HighlightEffect");
        }

        public override void LoadContent()
        {
            base.LoadContent();

            for(int i = 0; i < Math.Min(Buttons.Count, 6); ++i)
            {
                Buttons[i].Move(new Vector2(20 + (i / 2) * 420, 180 + (i % 2) * 220));
            }
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            if(previous != current)
            {
                (Buttons[previous] as MokeponOverview).Background.DeactivateEffect("HighlightEffect");
                (Buttons[current] as MokeponOverview).Background.ActivateEffect("HighlightEffect");
            }

            base.SwitchButtonSelection(previous, current);
        }
    }
}

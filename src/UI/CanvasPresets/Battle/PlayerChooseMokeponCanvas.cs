using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using MokeponGame.Gameplay;

namespace MokeponGame.UI.CanvasPresets.Battle
{
    public class PlayerChooseMokeponCanvas : MokeponListCanvas
    {
        public bool MokeponChosen;
        public int Choice;
        bool warning;
        Mokepon cur;

        public PlayerChooseMokeponCanvas(List<Mokepon> pokemons, Mokepon current = null) : base(pokemons)
        {
            cur = current;
            Elements.Add(new Text("Choose your Mokepon!", "Expression-pro-32px"));
            (Elements.Last() as Text).Color = Color.White;

            MokeponChosen = false;
            warning = false;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements.Last().MoveMid(new Vector2(Globals.ScreenWidth / 2, 75));
        }

        public override void ButtonOnClick(int buttonid)
        {
            base.ButtonOnClick(buttonid);

            if (MokeponChosen)
                return;

            if (poks[buttonid].HP <= 0)
                return;

            if(poks[buttonid] != cur)
            {
                MokeponChosen = true;
                Choice = buttonid;
            }
            else if(!warning)
            {
                warning = true;
                Text t = new Text("This Mokepon is already in battle!", Color.Red, Vector2.Zero, Vector2.One, "Expression-pro-32px");
                t.LoadContent();
                t.MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight - 75));
                Elements.Add(t);
            }
        }
    }
}

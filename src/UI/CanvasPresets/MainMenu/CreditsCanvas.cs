using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MokeponGame.UI.CanvasPresets
{
    public class CreditsCanvas : Canvas
    {
        public List<string> Credits;
        private int bottomid;
        private double speed;
        private double scrollDelay;
        private double scrollTo_wait;

        public CreditsCanvas() : base()
        {
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), 0.5f));
            Elements.Add(new Text("Credits:", "Expression-pro-32px"));
            (Elements[1] as Text).Color = Color.White;

            Credits = new List<string>();
            Credits.Add("Game by: Michal Maras");
            Credits.Add("Graphics:  Vicente Nitti (@vnitti)\nLink: https://itch.io/profile/vnitti");
            Credits.Add("Graphics: Eeve Somepx (@somepx)\nLink: https://somepx.itch.io/");
            Credits.Add("Graphics: Aekashics\nLink: http://www.akashics.moe/");
            Credits.Add("Graphics: Pipoya \nLink: https://pipoya.itch.io/");
            Credits.Add("Graphics: IPBP \n Link: https://www.fontspace.com/ipbp");
            Credits.Add("         Special Thanks: Coding Made Easy\nLink: https://www.youtube.com/user/CodingMadeEasy");
            Credits.Add("Special Thanks: The Mokepon Company\nLink: https://www.pokemon.com/");

            scrollDelay = 3.5;
            speed = 0.5;
            scrollTo_wait = 0;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));
        }

        public override void Update(GameTime gameTime)
        {
            scrollTo_wait -= gameTime.ElapsedGameTime.TotalSeconds;

            if(scrollTo_wait <= 0)
            {
                if(bottomid >= Credits.Count)
                {
                    (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("MainMenu");
                    return;
                }

                Text t = new Text(Credits[bottomid], "Expression-pro-32px");
                t.Color = Color.White;
                t.LoadContent();
                t.MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight));
                Elements.Add(t);

                ++bottomid;
                scrollTo_wait = scrollDelay;
                if(bottomid >= Credits.Count)
                {
                    scrollTo_wait += 2 * scrollDelay;
                }
            }

            for(int i = 2; i < Elements.Count; ++i)
            {
                Elements[i].MoveVector(new Vector2(0, -(float)speed));
            }

            while (Elements.Count > 2 && Elements[2].Position.Y < 200)
            {
                Elements[2].UnloadContent();
                Elements.RemoveAt(2);
            }

            if (Managers.InputManager.Instance.AnyKeysPressed(Keys.Escape, Keys.Enter, Keys.Space))
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("MainMenu");
                return;
            }

            base.Update(gameTime);
        }
    }
}

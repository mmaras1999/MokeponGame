using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MokeponGame.Managers;

namespace MokeponGame.UI.CanvasPresets
{
    public class IntroductionCanvas : Canvas
    {
        public List<string> Intro;
        private int bottomid;
        private double speed;
        private double scrollDelay;
        private double scrollTo_wait;

        public IntroductionCanvas() : base()
        {
            Elements.Add(new Image("MainMenu/MenuBackground1", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight), 0.5f));
            Elements.Add(new Text("Introduction:", "Expression-pro-32px"));

            Intro = new List<string>();
            Intro.Add("Welcome to the world of Mokepons!\n");
            Intro.Add("This dangerous, but incredible place is a natural habitat");
            Intro.Add("of many MOKEPON species. Those magical creatures can be  ");
            Intro.Add("found anywhere, even in the smallest berry bush! Some of ");
            Intro.Add("them wield great powers, abilities to manipulate forces  ");
            Intro.Add("of nature like water, earth and fire. Your task is to    ");
            Intro.Add("tame them and become a MOKEPON TRAINER! Should you be    ");
            Intro.Add("skilled enough, maybe one day you will take your chances ");
            Intro.Add("in the GRAND TOURNAMENT! Before that however, you should ");
            Intro.Add("gain more knowledge of MOKEPONS and how you use their    ");
            Intro.Add("magic to defeat any opponent you face! Worry not, you can");
            Intro.Add("find many beginner trainers eager to practice with you   ");
            Intro.Add("and your MOKEPONS. But first you will need to choose your");
            Intro.Add("first MOKEPON! What will it be?");

            scrollDelay = 2.5;
            speed = 0.5;
            scrollTo_wait = 0;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));
            (Elements[1] as Text).Color = Color.White;
        }

        public override void Update(GameTime gameTime)
        {
            scrollTo_wait -= gameTime.ElapsedGameTime.TotalSeconds;

            if (scrollTo_wait <= 0)
            {
                if (bottomid < Intro.Count)
                {
                    Text t = new Text(Intro[bottomid], "Expression-pro-32px");
                    t.LoadContent();
                    t.Color = Color.White;
                    t.MoveMid(new Vector2(Globals.ScreenWidth / 2, Globals.ScreenHeight));
                    Elements.Add(t);

                    ++bottomid;
                    scrollTo_wait = scrollDelay;
                    if (bottomid >= Intro.Count)
                    {
                        scrollTo_wait += 2 * scrollDelay;
                    }
                }
            }

            for (int i = 2; i < Elements.Count; ++i)
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
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("SelectMokepon");
                return;
            }

            if(Elements.Count == 2)
            {
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.MainMenuScreen).ChangeCanvas("SelectMokepon");
                return;
            }

            base.Update(gameTime);
        }
    }
}

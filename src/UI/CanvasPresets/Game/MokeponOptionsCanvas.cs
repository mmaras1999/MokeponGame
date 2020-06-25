using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using MokeponGame.Gameplay;

namespace MokeponGame.UI.CanvasPresets
{
    public class MokeponOptionsCanvas : Canvas
    {
        Mokepon mokepon;

        public MokeponOptionsCanvas(Mokepon mokepon)
        {
            Elements.Add(new Image("White", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            this.mokepon = mokepon;
            Elements.Add(new Text("Name: " + mokepon.Name, "Expression-pro-32px"));
            Elements.Add(new Text("Species: " + mokepon.DefaultName, "Expression-pro-32px"));
            Elements.Add(new Image("Mokepons/" + mokepon.DefaultName));
            Elements.Add(new Text("Statistics:", "Expression-pro-32px"));
            Elements.Add(new Text("Max Health: " + mokepon.MaxHP + "\nHealth: " + mokepon.HP + "\nAttack: " + mokepon.ATK +
                "\nDefense: " + mokepon.DEF + "\nSpecial Attack: " + mokepon.SP_ATK + "\nSpecial Defense: " + mokepon.SP_DEF +
                "\nSpeed: " + mokepon.SPD + "\nAccuracy: " + mokepon.ACC));

            Buttons.Add(new Button(null, new Text("Release")));
            Buttons.Add(new Button(null, new Text("Back")));
            ButtonRows = 2;
            Buttons[0].ButtonText.Color = Color.DimGray;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 50));
            Elements[2].MoveMid(new Vector2(800, 150));

            int maxSize = Math.Max((Elements[3] as Image).Width, (Elements[3] as Image).Height);
            Elements[3].Scale = Vector2.One * 350.0f / maxSize;
            Elements[3].MoveMid(new Vector2(250, 350));

            Elements[4].Move(new Vector2(675, 180));
            Elements[5].Move(new Vector2(675, 230));

            Buttons[0].MoveMid(new Vector2(Globals.ScreenWidth / 2, 600));
            Buttons[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 650));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(Managers.InputManager.Instance.KeysPressed(Keys.Escape))
            {
                Canvas c = new MokeponDisplayCanvas(Managers.GameManager.Instance.PlayerData.Mokepons);
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas(c);
                return;
            }
        }

        public override void ButtonOnClick(int buttonid)
        {
            base.ButtonOnClick(buttonid);
            
            if(buttonid == 0)
            {
                Managers.GameManager.Instance.PlayerData.Mokepons.Remove(mokepon);
                Canvas c = new MokeponDisplayCanvas(Managers.GameManager.Instance.PlayerData.Mokepons);
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas(c);
                return;
            }
            else if(buttonid == 1)
            {
                Canvas c = new MokeponDisplayCanvas(Managers.GameManager.Instance.PlayerData.Mokepons);
                (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas(c);
                return;
            }
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            if(previous != current)
            {
                Buttons[previous].ButtonText.Color = Color.Black;
                Buttons[current].ButtonText.Color = Color.DimGray;
            }

            base.SwitchButtonSelection(previous, current);
        }
    }
}

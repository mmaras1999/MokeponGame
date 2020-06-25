using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using MokeponGame.Managers;

namespace MokeponGame.UI.CanvasPresets.Battle
{
    public class NameCapturedMokeponCanvas : Canvas
    {
        public bool Finished;
        InsertStringCanvas insert;

        public NameCapturedMokeponCanvas()
        {
            insert = new InsertStringCanvas(GameManager.Instance.PlayerData.Mokepons.Last().Name);
            Elements.Add(new Image("game_background_2", new Rectangle(0, 0, Globals.ScreenWidth, Globals.ScreenHeight)));
            Elements.Add(new Text("Do you wish to name your new Mokepon?", "Expression-pro-32px"));
            Finished = false;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            Elements[1].MoveMid(new Vector2(Globals.ScreenWidth / 2, 100));
            insert.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (insert.Inserted)
            {
                GameManager.Instance.PlayerData.Mokepons.Last().Name = insert.Input;
                GameManager.Instance.SaveGame();
                Finished = true;
                return;
            }

            insert.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            insert.Draw(spriteBatch);
        }

        public override void ButtonOnClick(int buttonid)
        {
            base.ButtonOnClick(buttonid);
        }

        public override void SwitchButtonSelection(int previous, int current)
        {
            base.SwitchButtonSelection(previous, current);
        }
    }
}

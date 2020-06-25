using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MokeponGame.Gameplay.GameMap
{
    public class MapLayer
    {
        public string SheetPath;
        public int Width;
        public int Height;
        public List<Tile> Tiles;

        [XmlIgnore]
        ContentManager content;
        [XmlIgnore]
        Texture2D tileSheet;

        int GetIndex(int x, int y)
        {
            return x + y * Width;
        }

        public MapLayer()
        {
            
        }

        public void LoadContent()
        {
            content = new ContentManager(Managers.ScreenManager.Instance.Content.ServiceProvider, "Content");
            tileSheet = content.Load<Texture2D>(SheetPath);
        }

        public void UnloadContent()
        {
            content.Unload();
        }

        public void Draw(SpriteBatch spriteBatch, int x0, int y0, Vector2 translation)
        {
            Vector2 Mid = new Vector2(Globals.ScreenWidth / 2 - 32, Globals.ScreenHeight / 2 - 32);

            for(int i = x0 - 10; i <= x0 + 11; ++i)
            {
                for(int j = y0 - 6; j <= y0 + 7; ++j)
                {
                    if(i >= 0 && j >= 0 && i < Width && j < Height && Tiles[GetIndex(i, j)].SheetX >= 0)
                    {
                        spriteBatch.Draw(tileSheet, 
 
                            new Rectangle((int)(64 * (i - x0) + Mid.X + translation.X * 64), 
                                          (int)(64 * (j - y0) + Mid.Y + translation.Y * 64), 
                                          64, 
                                          64),

                            new Rectangle(32 * Tiles[GetIndex(i, j)].SheetX, 
                                          32 * Tiles[GetIndex(i, j)].SheetY, 
                                          Tiles[GetIndex(i, j)].Width, 
                                          Tiles[GetIndex(i, j)].Width),
                            Color.White);
                    }
                }
            }
        }
    }
}

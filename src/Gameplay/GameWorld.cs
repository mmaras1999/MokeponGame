using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using MokeponGame.Gameplay.GameMap;
using MokeponGame.Managers;
using MokeponGame.Gameplay.WorldObjects;

namespace MokeponGame.Gameplay
{
    public class GameWorld
    {
        public string Name;
        public int Width;
        public int Height;

        public List<MapLayer> Layers;
        public List<bool> NavMesh;

        [XmlArrayItem(typeof(DialogueObject))]
        [XmlArrayItem(typeof(MokeponSpawnObject))]
        [XmlArrayItem(typeof(MapTransition))]
        [XmlArrayItem(typeof(RestorePoint))]
        [XmlArrayItem(typeof(Shop))]
        public List<WorldObject> WorldObjects;

        [XmlIgnore]
        public List<WorldObject>[,] Objects;

        public GameWorld()
        {

        }

        public bool OnBoard(int x, int y)
        {
            return (x >= 0 && y >= 0 && x < Width && y < Height);
        }

        public bool Passable(int x, int y)
        {
            return (OnBoard(x, y) && !NavMesh[x + y * Width]);
        }

        public void TriggerActions(int x, int y)
        {
            if (!OnBoard(x, y) || Objects[x, y] == null)
                return;
            foreach(var obj in Objects[x, y])
            {
                obj.Action();
            }
        }

        public void TriggerStepped(int x, int y)
        {
            if (!OnBoard(x, y) || Objects[x, y] == null)
                return;
            foreach(var obj in Objects[x, y])
            {
                obj.Stepped();
            }
        }

        public void LoadContent()
        {
            foreach (var layer in Layers)
                layer.LoadContent();

            Objects = new List<WorldObject>[Width, Height];

            foreach (var obj in WorldObjects)
            {
                if (Objects[obj.PositionX, obj.PositionY] == null)
                    Objects[obj.PositionX, obj.PositionY] = new List<WorldObject>();
                Objects[obj.PositionX, obj.PositionY].Add(obj);
            }
        }

        public void UnloadContent()
        {
            foreach (var layer in Layers)
                layer.UnloadContent();
            GC.Collect();
        }

        public void Update(GameTime gameTime)
        {
            //UPDATE NPC, ETC.
        }

        public void Draw(SpriteBatch spriteBatch, int x0, int y0, Vector2 translation)
        {
            foreach (var layer in Layers)
                layer.Draw(spriteBatch, x0, y0, translation);
        }
    }
}

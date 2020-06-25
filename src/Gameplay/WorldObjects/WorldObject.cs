using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using MokeponGame.Gameplay.WorldObjects;

namespace MokeponGame.Gameplay
{
    public class WorldObject
    {
        public int PositionX;
        public int PositionY;

        public virtual void Action()
        {

        }

        public virtual void Stepped()
        {

        }
    }
}

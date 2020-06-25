using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay
{
    public class Item
    {
        public string Name;
        public int Cost;
        public string AbilityName;
        public int Amount;
        public string Description;

        public Item()
        {
            Amount = 1;
        }
    }
}

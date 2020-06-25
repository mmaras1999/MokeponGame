using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay
{
    public class Enemy
    {
        public string Name;
        public List<Mokepon> Mokepons;
        public string TauntText;
        public string MokeponChoiceText;
        public int AILevel;
        
        public Enemy()
        {
            Name = "";
            Mokepons = new List<Mokepon>();
            AILevel = 0;
        }
    }
}

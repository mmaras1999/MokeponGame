using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay.Abilities
{
    public class Bite : Ability
    {
        public Bite()
        {
            Name = "Bite";
            DMG = 40;
            ACC = 100;
            Attack = true;
            Special = false;
            AbilityType = MokeponTypes.NORMAL;
        }
    }
}

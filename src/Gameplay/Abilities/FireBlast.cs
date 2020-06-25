using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay.Abilities
{
    public class FireBlast : Ability
    {
        public FireBlast()
        {
            Name = "Fire Blast";
            ACC = 90;
            DMG = 30;
            Attack = true;
            Special = false;
            AbilityType = MokeponTypes.FIRE;
        }
    }
}

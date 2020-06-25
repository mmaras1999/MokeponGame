using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay.Abilities
{
    public class Scratch : Ability
    {
        public Scratch()
        {
            Name = "Scratch";
            DMG = 40;
            ACC = 100;
            Attack = true;
            Special = false;
            AbilityType = MokeponTypes.NORMAL;
        }
    }
}

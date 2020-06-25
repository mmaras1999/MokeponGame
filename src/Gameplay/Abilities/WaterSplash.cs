using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay.Abilities
{
    public class WaterSplash : Ability
    {
        public WaterSplash()
        {
            Name = "Water Splash";
            ACC = 90;
            DMG = 30;
            Attack = true;
            Special = false;
            AbilityType = MokeponTypes.WATER;
        }
    }
}

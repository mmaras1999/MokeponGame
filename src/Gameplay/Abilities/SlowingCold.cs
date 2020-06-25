using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MokeponGame.Screens;

namespace MokeponGame.Gameplay.Abilities
{
    public class SlowingCold : Ability
    {
        public SlowingCold()
        {
            Name = "Slowing Cold";
            DMG = 0;
            ACC = 90;
            Attack = false;
            Special = false;
            AbilityType = MokeponTypes.ICE;
            Priority = 0;
        }

        public override void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {
            target.AddBuff(0, 0, 0, 0, -20, -20);
            screen.Dialogues.AddText(caster.Name + "\'s cold has slowed " + target.Name + "! ");
            screen.Dialogues.AddText(target.Name + "\'s speed and accuracy fell!");
        }
    }
}

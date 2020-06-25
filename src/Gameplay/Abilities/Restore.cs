using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MokeponGame.Screens;

namespace MokeponGame.Gameplay.Abilities
{
    public class Restore : Ability
    {
        public Restore()
        {
            Name = "Restore";
            DMG = 0;
            ACC = 100;
            Attack = false;
            Special = true;
            AbilityType = MokeponTypes.NONE;
            Priority = 1;
        }

        public override void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {
            caster.EraseBattleBuffs();
            screen.Dialogues.AddText(caster.Name + " attributes have been reset!");
        }
    }
}

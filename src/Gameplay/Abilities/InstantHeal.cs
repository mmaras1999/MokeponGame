using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MokeponGame.Screens;

namespace MokeponGame.Gameplay.Abilities
{
    public class InstantHeal : Ability
    {
        public InstantHeal() //HEALS 25 HP POINTS
        {
            Name = "InstantHeal";
            DMG = 0;
            ACC = 100;
            Attack = false;
            Special = false;
            AbilityType = MokeponTypes.NONE;
            Priority = 1;
        }

        public override void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {
            int prevHP = caster.HP;
            caster.Heal(25);
            screen.Dialogues.AddText(caster.Name + " restored " + (caster.HP - prevHP).ToString() + " health points!");
        }
    }
}

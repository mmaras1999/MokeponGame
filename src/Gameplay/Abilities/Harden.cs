using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MokeponGame.Screens;

namespace MokeponGame.Gameplay.Abilities
{
    public class Harden : Ability
    {
        public Harden()
        {
            Name = "Harden";
            DMG = 0;
            ACC = 100;
            Attack = false;
            Special = false;
            AbilityType = MokeponTypes.NONE;
            Priority = 0;
        }

        public override void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {
            caster.AddBuff(0, 5, 0, 0, 0, 0);
            screen.Dialogues.AddText(caster.Name + "\'s defence rose!");
        }
    }
}

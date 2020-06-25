using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MokeponGame.Screens;

namespace MokeponGame.Gameplay.Abilities
{
    public class Blizzard : Ability
    {
        public Blizzard()
        {
            Name = "Blizzard";
            ACC = 20;
            DMG = 80;
            Attack = true;
            Special = true;
            AbilityType = MokeponTypes.ICE;
        }

        public override void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {
            target.AddBuff(0, 0, 0, 0, -10, -10);
            screen.Dialogues.AddText("Blizzard slowed " + target.Name + "!");
            screen.Dialogues.AddText(target.Name + "\'s speed and accuracy fell!");
        }
    }
}

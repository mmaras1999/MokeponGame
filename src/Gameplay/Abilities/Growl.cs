using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MokeponGame.Screens;

namespace MokeponGame.Gameplay.Abilities
{
    public class Growl : Ability
    {
        public Growl()
        {
            Name = "Growl";
            DMG = 0;
            ACC = 100;
            Attack = false;
            Special = false;
            AbilityType = MokeponTypes.NONE;
            Priority = 0;
        }

        public override void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {
            screen.Dialogues.AddText(target.Name + "\'s attack fell!");
            target.AddBuff(-3, 0, 0, 0, 0, 0);
        }
    }
}

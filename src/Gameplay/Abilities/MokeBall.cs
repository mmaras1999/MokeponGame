using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MokeponGame.Screens;

namespace MokeponGame.Gameplay.Abilities
{
    public class MokeBall : Ability
    {
        public MokeBall()
        {
            Name = "Catch";
            DMG = 0;
            ACC = 100;
            Attack = false;
            Special = true;
            AbilityType = MokeponTypes.NONE;
            Priority = 1;
        }

        public override void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {
            if(screen.enemy.Name.Length > 0)
            {
                screen.Dialogues.AddText("How dare you try to catch someone's Mokepon?!");
                return;
            }

            int rand = Managers.GameManager.Instance.random.Next(256);
            int treshold = (target.HP * 255 * target.LVL) / (target.MaxHP * 4);

            if(rand < treshold)
            {
                screen.Dialogues.AddText(target.Name + " broke free!");
                return;
            }
            else
            {
                screen.EnemyMokeponCaught = true;
                return;
            }
        }
    }
}

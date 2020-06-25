using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay
{
    public static class EnemyAI
    {
        public static string ChooseMove(Mokepon attacker, Mokepon defender, int level)
        {
            if(level == 0)
            {
                return attacker.AbilityNames[Managers.GameManager.Instance.random.Next(0, attacker.AbilityNames.Count)];
            }

            return attacker.AbilityNames[0];
        }
    }
}

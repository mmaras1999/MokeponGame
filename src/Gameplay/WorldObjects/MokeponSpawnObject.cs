using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay.WorldObjects
{
    public class MokeponSpawnObject : WorldObject
    {
        public class SpawnInfo
        {
            public string MokeponName;
            public int Probability; 
        }

        public List<SpawnInfo> Spawns;
        public int Probability;
        public int MinLevel;
        public int MaxLevel;

        public override void Stepped()
        {
            base.Stepped();

            if (Probability <= Managers.GameManager.Instance.random.Next(100))
                return;

            int totalPpb = 0;

            Console.WriteLine("Spawn count: " + Spawns.Count.ToString());

            foreach(var spawn in Spawns)
            {
                totalPpb += spawn.Probability;
            }

            int randint = Managers.GameManager.Instance.random.Next(0, totalPpb);

            foreach(var spawn in Spawns)
            {
                if(randint < spawn.Probability)
                {
                    int level = Managers.GameManager.Instance.random.Next(MinLevel, MaxLevel + 1);

                    Mokepon wildMokepon = new Mokepon(spawn.MokeponName, level);

                    Enemy enemy = new Enemy();
                    enemy.Mokepons.Add(wildMokepon);
                    enemy.MokeponChoiceText = "Wild {0} has appeared!";
                    enemy.AILevel = 0;

                    (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).StartBattle(enemy);
                    return;
                }
                randint -= spawn.Probability;
            }
        }
    }
}

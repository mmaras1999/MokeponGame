using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay
{
    public class BattleBuff
    {
        public int ATK;
        public int DEF;
        public int SP_ATK;
        public int SP_DEF;
        public int SPD;
        public int ACC;

        public BattleBuff()
        {
            ATK = DEF = SP_ATK = SP_DEF = SPD = ACC = 0;
        }

        public BattleBuff(int atk, int def, int sp_atk, int sp_def, int spd, int acc)
        {
            ATK = atk;
            DEF = def;
            SP_ATK = sp_atk;
            SP_DEF = sp_def;
            SPD = spd;
            ACC = acc;
        }
    }
}

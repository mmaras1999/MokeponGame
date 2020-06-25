using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay
{
    public class Damage
    {
        public bool Critical;
        public double Multipiler;
        public int DamageValue;

        public Damage(bool critical, double multipiler, int damageValue)
        {
            Critical = critical;
            Multipiler = multipiler;
            DamageValue = damageValue;
        }
    }
}

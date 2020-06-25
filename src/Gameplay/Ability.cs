using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MokeponGame.Screens;

namespace MokeponGame.Gameplay
{
    public class Ability
    {
        public string Name;
        public int DMG; //Damage
        public int ACC; //Accuracy 0-100
        public int Priority;
        public bool Special;
        public bool Attack;
        public bool UsedWithItem;
        public string ItemName;
        public MokeponTypes AbilityType;

        public Ability()
        {
            DMG = 0;
            ACC = 100;
            Priority = 0;
            Special = false;
            Attack = false;
            AbilityType = MokeponTypes.NONE;
            UsedWithItem = false;
            ItemName = "";
        }

        public virtual void Effect(ref Mokepon caster, ref Mokepon target, BattleScreen screen)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

using MokeponGame.Managers;

namespace MokeponGame.Gameplay
{
    public class Mokepon
    {
        public Mokepon(string mokeponName, int level)
        {
            Mokepon basemokepon = XmlManager.Instance.Load<Mokepon>("Data/Mokepons/BaseStats/" + mokeponName + ".xml");
            Name = basemokepon.DefaultName;
            DefaultName = basemokepon.DefaultName;

            //Generate IVs
            HP_IV = GameManager.Instance.random.Next(0, 16);
            ATK_IV = GameManager.Instance.random.Next(0, 16);
            DEF_IV = GameManager.Instance.random.Next(0, 16);
            SP_ATK_IV = GameManager.Instance.random.Next(0, 16);
            SP_DEF_IV = GameManager.Instance.random.Next(0, 16);
            SPD_IV = GameManager.Instance.random.Next(0, 16);

            //Level and experience to level
            LVL = level;
            LVL_UP_XP = 5 * LVL * LVL * LVL / 4;
            XP = 0;

            //Calculate stats
            MaxHP = (2 * (basemokepon.MaxHP + HP_IV) * LVL) / 100 + LVL + 10;
            ATK = (2 * (basemokepon.ATK + ATK_IV) * LVL) / 100 + 5;
            DEF = (2 * (basemokepon.DEF + DEF_IV) * LVL) / 100 + 5;
            SP_ATK = (2 * (basemokepon.SP_ATK + SP_ATK_IV) * LVL) / 100 + 5;
            SP_DEF = (2 * (basemokepon.SP_DEF + SP_DEF_IV) * LVL) / 100 + 5;
            SPD = (2 * (basemokepon.SPD + SPD_IV) * LVL) / 100 + 5;
            ACC = 100;

            HP = MaxHP;

            Type1 = basemokepon.Type1;
            Type2 = basemokepon.Type2;
            EvolutionName = basemokepon.EvolutionName;
            EvolutionLevel = basemokepon.EvolutionLevel;
            AbilityNames = basemokepon.AbilityNames;
            BattleBuffs = new List<BattleBuff>();
        }

        public Mokepon()
        {
            //DEFAULT CONSTRUCTOR FOR XML SERIALIZATION
            BattleBuffs = new List<BattleBuff>();
        }

        //NAME
        public string Name;
        public string DefaultName;

        //HEALTH
        public int MaxHP;
        public int HP;

        //LEVEL AND EXPERIENCE
        public int LVL;
        public int LVL_UP_XP; // 5/4 * LVL^3
        public int XP;

        public int ATK;
        public int DEF;
        public int SP_ATK;
        public int SP_DEF;
        public int SPD;
        public int ACC;

        //Individual Values
        public int HP_IV;
        public int ATK_IV;
        public int DEF_IV;
        public int SP_ATK_IV;
        public int SP_DEF_IV;
        public int SPD_IV;

        public MokeponTypes Type1;
        public MokeponTypes Type2;

        public string EvolutionName;
        public int EvolutionLevel;

        [XmlArray()]
        [XmlArrayItem(ElementName = "AbilityName")]
        public List<string> AbilityNames;

        //DEBUFF LIST

        [XmlIgnore]
        public List<BattleBuff> BattleBuffs;

        public void Restore()
        {
            HP = MaxHP;
            EraseBattleBuffs();
        }

        public void GainXP(int value)
        {
            XP += value;
        }

        public void ReceiveDamage(int damage)
        {
            HP = Math.Max(0, HP - damage);
        }

        public void LevelUp()
        {
            Mokepon basemokepon = XmlManager.Instance.Load<Mokepon>("Data/Mokepons/BaseStats/" + DefaultName + ".xml");

            //Level and experience to level
            LVL++;
            LVL_UP_XP = 5 * LVL * LVL * LVL / 4;

            //Calculate stats
            int prevMaxHP = MaxHP;
            MaxHP = (2 * (basemokepon.MaxHP + HP_IV) * LVL) / 100 + LVL + 10;
            ATK = (2 * (basemokepon.ATK + ATK_IV) * LVL) / 100 + 5;
            DEF = (2 * (basemokepon.DEF + DEF_IV) * LVL) / 100 + 5;
            SP_ATK = (2 * (basemokepon.SP_ATK + SP_ATK_IV) * LVL) / 100 + 5;
            SP_DEF = (2 * (basemokepon.SP_DEF + SP_DEF_IV) * LVL) / 100 + 5;
            SPD = (2 * (basemokepon.SPD + SPD_IV) * LVL) / 100 + 5;

            HP += MaxHP - prevMaxHP;

            //RESTORE BUFFS
            List<BattleBuff> temp = BattleBuffs;
            BattleBuffs = new List<BattleBuff>();
            
            foreach (var buff in temp)
            {
                AddBuff(buff.ATK, buff.DEF, buff.SP_ATK, buff.SP_DEF, buff.SPD, buff.ACC);
            }

            //IF EVOLUTION LEVEL == LVL
            //EVOLVE

            //ADD NEW ABILITIES
        }

        public void EraseBattleBuffs()
        {
            foreach (var buff in BattleBuffs)
            {
                ATK -= buff.ATK;
                DEF -= buff.DEF;
                SP_ATK -= buff.SP_ATK;
                SP_DEF -= buff.SP_DEF;
                SPD -= buff.SPD;
                ACC -= buff.ACC;
            }

            BattleBuffs.Clear();
        }

        public void AddBuff(int atk, int def, int sp_atk, int sp_def, int spd, int acc)
        {
            int prevATK = ATK;
            int prevDEF = DEF;
            int prevSP_ATK = SP_ATK;
            int prevSP_DEF = SP_DEF;
            int prevSPD = SPD;
            int prevACC = ACC;

            ATK = Math.Min(100, Math.Max(1, ATK + atk));
            DEF = Math.Min(100, Math.Max(1, DEF + def));
            SP_ATK = Math.Min(100, Math.Max(1, SP_ATK + sp_atk));
            SP_DEF = Math.Min(100, Math.Max(1, SP_ATK + sp_atk));
            SPD = Math.Min(100, Math.Max(1, SPD + spd));
            ACC = Math.Min(100, Math.Max(1, ACC + acc));

            BattleBuff debuff = new BattleBuff(ATK - prevATK, DEF - prevDEF, SP_ATK - prevSP_ATK,
                SP_DEF - prevSP_DEF, SPD - prevSPD, ACC - prevACC);
            BattleBuffs.Add(debuff);
        }

        public void Heal(int value)
        {
            HP = Math.Min(MaxHP, HP + value);
        }
    }
}

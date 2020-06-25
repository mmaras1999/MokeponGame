using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

using MokeponGame.Gameplay;

namespace MokeponGame.Managers
{
    public class GameManager
    {
        //Singleton
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();
                return instance;
            }
        }

        public Player PlayerData; //Player's Character Data
        public int GameSaveID; //ID of save slot
        public GameWorld World; //Current World
        public Random random; //Random engine

        private GameManager()
        {
            PlayerData = null;
            GameSaveID = -1;
            random = new Random();
        }

        //LOAD SAVED GAME
        public void LoadSave(int saveID)
        {
            if (!File.Exists("Data/save" + saveID.ToString() + ".xml"))
                throw new IOException("File does not exists!");
            GameSaveID = saveID;
            PlayerData = Managers.XmlManager.Instance.Load<Player>("Data/save" + GameSaveID.ToString() + ".xml");
        }

        //SAVE GAME
        public void SaveGame()
        {
            if (GameSaveID != -1 && PlayerData != null)
            {
                Managers.XmlManager.Instance.Save<Player>(PlayerData, "Data/save" + GameSaveID.ToString() + ".xml");
            }
        }

        //LOAD WORLD
        public void LoadWorld(string name)
        {
            World = XmlManager.Instance.Load<GameWorld>("Data/Locations/" + name + ".xml");
            GC.Collect();
        }

        public Damage CalculateDamage(Mokepon attacker, Mokepon defender, Ability ability)
        {
            if (!ability.Attack) //SHOULDN'T HAPPEN, EVER
                return null;

            int critPpb = defender.SPD / 2;
            bool crit = (Managers.GameManager.Instance.random.Next(0, 256) < critPpb);

            double damage = (2.0 * (crit ? 2.0 : 1.0) / 5.0 + 2.0);

            damage *= ability.Special ? (attacker.SP_ATK / (double)defender.SP_DEF) : (attacker.ATK / (double)defender.DEF);
            damage /= 50;
            damage += 2;

            damage *= Managers.GameManager.Instance.random.NextDouble() * 0.15 + 0.85; //random double from 0.85 to 1.0

            double multip = 1;
            multip *= Globals.MokeponValueChart[(int)ability.AbilityType, (int)defender.Type1];
            multip *= Globals.MokeponValueChart[(int)ability.AbilityType, (int)defender.Type2];
            damage *= multip;

            if (attacker.Type1 == ability.AbilityType || attacker.Type2 == ability.AbilityType)
            {
                damage *= 1.5;
            }

            return new Damage(crit, multip, (int)Math.Round(damage));
        }

        public int CalculateExperience(Mokepon attacker, Mokepon defender, bool wild = true)
        {
            double exp = 1;

            if (!wild)
                exp = 1.5;

            exp *= defender.LVL / (double)attacker.LVL;
            exp *= defender.LVL;

            return (int)Math.Ceiling(exp);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace MokeponGame.Gameplay
{
    public class Player : WorldObject
    {
        public string Name;
        public int BodyType;
        public int Money;
        public List<Mokepon> Mokepons;
        public List<Mokepon> Storage; // TO BE DONE
        public List<Item> Items;

        public string Location;
        public int Direction;

        public Player()
        {
            Name = "";
            BodyType = 0;
            Mokepons = new List<Mokepon>();
            Storage = new List<Mokepon>();
            Items = new List<Item>();
            Money = 0;
            Location = "TestLocation";
            PositionX = 4;
            PositionY = 4;
            Direction = 0;
        }

        public void AddItem(Item item)
        {
            var found = Items.Find(it => it.Name == item.Name);
            
            if(found != null)
            {
                found.Amount++;
            }
            else
            {
                Item temp = Managers.XmlManager.Instance.Load<Item>("Data/Items/" + item.Name.Replace(" ", "") + ".xml");
                Items.Add(temp);
            }
        }

        public void DeleteItem(Item item)
        {
            foreach (var it in Items)
            {
                if(it.Name == item.Name)
                {
                    it.Amount--;
                }
            }

            Items.RemoveAll(it => it.Amount <= 0);
        }
    }
}

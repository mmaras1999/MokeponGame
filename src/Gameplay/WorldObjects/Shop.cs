using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using MokeponGame.UI.CanvasPresets;

namespace MokeponGame.Gameplay.WorldObjects
{
    public class Shop : WorldObject
    {
        [XmlArrayItem(ElementName ="Item")]
        [XmlArray]
        public List<string> AvailableItems;

        public Shop()
        {
            AvailableItems = new List<string>();
        }

        public override void Action()
        {
            ShopCanvas c = new ShopCanvas(Managers.GameManager.Instance.PlayerData, this);
            (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeCanvas(c);
        }
    }
}

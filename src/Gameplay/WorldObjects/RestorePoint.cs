using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay.WorldObjects
{
    public class RestorePoint : DialogueObject
    {
        public override void Action()
        {
            foreach(var pok in Managers.GameManager.Instance.PlayerData.Mokepons)
            {
                pok.Restore();
            }
            base.Action();
        }
    }
}

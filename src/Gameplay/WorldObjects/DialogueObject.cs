using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay.WorldObjects
{
    public class DialogueObject : WorldObject
    {
        public string DialogueMessage;

        public override void Action()
        {
            base.Action();
            (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).DisplayDialogue(DialogueMessage);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame.Gameplay.WorldObjects
{
    public class MapTransition : WorldObject
    {
        public string NextMap;
        public int StartX;
        public int StartY;
        public int Dir;

        public override void Stepped()
        {
            base.Stepped();
            (Managers.ScreenManager.Instance.CurrentScreen as Screens.GameScreen).ChangeMap(NextMap, Dir, StartX, StartY);
        }
    }
}

using System;

namespace MokeponGame
{
#if WINDOWS || LINUX
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new MokeGame())
                game.Run();
        }
    }
#endif
}

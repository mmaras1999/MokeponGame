using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MokeponGame
{
    public static class Globals
    {
        public static int ScreenWidth = 1280; //SCREEN WIDTH
        public static int ScreenHeight = 720; //SCREEN HEIGHT
        public static int CharacterTypeCount = 36; //NUMBER OF CHARACTER ASSETS
        public static int INF = (int)2e9 + 7; //INFINITY

        //MULTIPILERS ARRAY FOR MOKEPON BATTLE
        public static double[,] MokeponValueChart = { { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                                                      { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5, 0, 1, 1, 0.5},
                                                      { 1, 1, 0.5, 0.5, 2, 1, 2, 1, 1, 1, 1, 1, 2, 0.5, 1, 0.5, 1, 2},
                                                      { 1, 1, 2, 0.5, 0.5, 1, 1, 1, 1, 2, 1, 1, 1, 2, 1, 0.5, 1, 1},
                                                      { 1, 1, 0.5, 2, 0.5, 1, 1, 1, 0.5, 2, 0.5, 1, 0.5, 2, 1, 0.5, 1, 0.5},
                                                      { 1, 1, 1, 2, 0.5, 0.5, 1, 1, 1, 0, 2, 1, 1, 1, 1, 0.5, 1, 1},
                                                      { 1, 1, 0.5, 0.5, 2, 1, 0.5, 1, 1, 2, 2, 1, 1, 1, 1, 2, 1, 0.5},
                                                      { 1, 2, 1, 1, 1, 1, 2, 1, 0.5, 1, 0.5, 0.5, 0.5, 2, 0, 1, 2, 2},
                                                      { 1, 1, 1, 1, 2, 1, 1, 1, 0.5, 0.5, 1, 1, 1, 0.5, 0.5, 1, 1, 0},
                                                      { 1, 1, 2, 1, 0.5, 2, 1, 1, 2, 1, 0, 1, 0.5, 2, 1, 1, 1, 2},
                                                      { 1, 1, 1, 1, 2, 0.5, 1, 2, 1, 1, 1, 1, 2, 0.5, 1, 1, 1, 0.5},
                                                      { 1, 1, 1, 1, 1, 1, 1, 2, 2, 1, 1, 0.5, 1, 1, 1, 1, 0, 0.5},
                                                      { 1, 1, 2, 1, 1, 1, 2, 0.5, 1, 0.5, 2, 1, 2, 1, 1, 1, 1, 0.5},
                                                      { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 2, 1, 0.5, 0.5},
                                                      { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 0.5},
                                                      { 1, 1, 1, 1, 1, 1, 1, 0.5, 1, 1, 1, 2, 1, 1, 2, 1, 0.5, 0.5},
                                                      { 1, 1, 0.5, 0.5, 1, 1, 2, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0.5} };

        //LIST OF POKEMONS
        public static Dictionary<MokeponTypes, List<string>> MokeponLists = new Dictionary<MokeponTypes, List<string>>
                {
                    { MokeponTypes.FIRE, new List<string>(){"Candle"} },
                    { MokeponTypes.WATER, new List<string>(){"Octave", "Toxanha"} },
                    { MokeponTypes.GRASS, new List<string>(){"Bushy" } },
                    { MokeponTypes.ICE, new List<string>(){"Frostbite" } }
                };
    }
}

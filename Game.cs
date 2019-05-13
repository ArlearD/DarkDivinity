using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Digger
{
    public static class Game
    {
        private const string mapWithPlayerTerrain = @"
TTT T
TTP T
T T T
TT TT";

        private const string mapWithPlayerTerrainSackGold = @"
PTTGTT TS
TST  TSTT
TTTTTTSTT
T TSTS TT
T TTTG ST
TSTSTT TT";

        private const string mapWithPlayerTerrainSackGoldMonster = @"
PTTGTT TST
TST  TSTTM
TTT TTSTTT
T TSTS TTT
T TTTGMSTS
T TMT M TS
TSTSTTMTTT
S TTST  TG
 TGST MTTT
 T  TMTTTT";

        private const string newMap = @"
TTTTTTTTTTTTTTTTTTTT
                    
              T     
              T     
           TTTTT    
 P        T        T
TTTTTTTTTTTTTTTTTTTT";

        private const string newMap2 = @"
TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT
T                                      T
T                                      T
T                                      T
T                      TTTT            T
T                                      T
T                           T          T
T                                      T
T                             T        T
T                                      T
T                               T      T
T                                      T
T                                 T    T
T                                    TTT
T                  TTT   TTT   TTT     T
T                TTT                   T
T    P         T       SSSSSSSSSSSSSSSST
TTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTTT";
        public static ICreature[,] Map;
        public static int Scores;
        public static bool IsOver;

        public static Keys KeyPressed;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        public static void CreateMap()
        {
            Map = CreatureMapCreator.CreateMap(newMap);
        }

        public static List<Point> GetPosition(ICreature cr)
        {
            List<Point> list = new List<Point>();
            for (int i = 0; i < MapWidth; i++)
            {
                for (int k = 0; k < MapHeight; k++)
                {
                    if (Map[i,k] != null && Map[i,k].ToString() == cr.ToString())
                    {
                        list.Add(new Point(i, k));
                    }
                }
            }
            return list;
        }
    }
}
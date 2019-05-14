using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Digger
{
    public static class Game
    {


        public static ICreature[,] Map;
        public static int Scores;
        public static bool IsOver;

        public static Keys KeyPressed;
        public static int MapWidth => Map.GetLength(0);
        public static int MapHeight => Map.GetLength(1);

        public static void CreateMap(string map)
        {
            Map = CreatureMapCreator.CreateMap(map);
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
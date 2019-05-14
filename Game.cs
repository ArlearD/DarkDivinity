using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DarkDivinity
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
        public static bool Check(int x, int y, int moveX, int moveY)
        {
            return Game.Map[x + moveX, y + moveY] != null &&
                (Game.Map[x + moveX, y + moveY].GetImageFileName() == "Spike.png"
                || Game.Map[x + moveX, y + moveY].GetImageFileName() == "Terrain.png"
                || Game.Map[x + moveX, y + moveY].GetImageFileName() == "Monster.png");
        }

        public static List<Point> GetPosition(string cr)
        {
            List<Point> list = new List<Point>();
            for (int i = 0; i < MapWidth; i++)
            {
                for (int k = 0; k < MapHeight; k++)
                {
                    if (Map[i,k] != null && Map[i,k].ToString() == cr)
                    {
                        list.Add(new Point(i, k));
                    }
                }
            }
            return list;
        }
    }
}
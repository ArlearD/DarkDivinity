using System;
using System.Windows.Forms;

namespace DarkDivinity
{
    internal static class Program
    {
        static int CurrentMap = 0;
        [STAThread]
        private static void Main()
        {
            Maps.AddMaps();
            NextMap();
            var form = new DarkDivinityWindow();
            Application.Run(form);
        }

        public static void NextMap()
        {

            Game.CreateMap(Maps.maps[CurrentMap]);
            CurrentMap++;

        }

        public static void SameMap()
        {
            CurrentMap--;
            Game.CreateMap(Maps.maps[CurrentMap]);
            CurrentMap++;
        }
    }
}
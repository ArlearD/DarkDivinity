using System;
using System.Windows.Forms;

namespace Digger
{
    internal static class Program
    {
        static int CurrentMap = 0;
        [STAThread]
        private static void Main()
        {
            Maps.AddMaps();
            NextMap();
            var form = new DiggerWindow();
            Application.Run(form);
        }
        public static void NextMap()
        {
           
            Game.CreateMap(Maps.maps[CurrentMap]);
            CurrentMap++;
           
        }
    }
}
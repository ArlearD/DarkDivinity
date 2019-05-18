using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace DarkDivinity
{
    public class DarkDivinityWindow : Form
    {
        private readonly Dictionary<string, Bitmap> bitmaps = new Dictionary<string, Bitmap>();
        public static GameState gameState;
        private readonly HashSet<Keys> pressedKeys = new HashSet<Keys>();
        private int tickCount;



        public DarkDivinityWindow(DirectoryInfo imagesDirectory = null)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.UserPaint, true);
            UpdateStyles();
            gameState = new GameState();
            ClientSize = new Size(
                GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight);
            if (imagesDirectory == null)
                imagesDirectory = new DirectoryInfo("Images");
            var files = imagesDirectory.GetFiles("*.png");
            foreach (var e in files)
                bitmaps[e.Name] = (Bitmap)Image.FromFile(e.FullName);
            var timer = new Timer
            {
                Interval = 15//15
            };
            timer.Tick += TimerTick;
            timer.Start();
        }

        protected override void OnLoad(EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer(@"C:\Users\Granter\Desktop\Новая папка (3)\Theme.wav");
            simpleSound.Play();
            base.OnLoad(e);
            Text = "DarkDivinity";
            DoubleBuffered = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            pressedKeys.Add(e.KeyCode);
            Game.KeyPressed = e.KeyCode;
        }



        protected override void OnKeyUp(KeyEventArgs e)
        {
            pressedKeys.Remove(e.KeyCode);
            Game.KeyPressed = pressedKeys.Any() ? pressedKeys.Min() : Keys.None;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Black, 0, 0, GameState.ElementSize * Game.MapWidth,
                GameState.ElementSize * Game.MapHeight);

            foreach (var a in gameState.Animations)
            {
                bitmaps[a.Creature.GetImageFileName()].MakeTransparent();
                e.Graphics.DrawImage(bitmaps[a.Creature.GetImageFileName()], a.Location);
            }
            e.Graphics.ResetTransform();
        }

        private void TimerTick(object sender, EventArgs args)
        {

            var portal = Game.GetPosition("DarkDivinity.Exit").FirstOrDefault();
            try
            {
                var player = Game.GetPosition("DarkDivinity.Player").First();
                if (player.X + 1 == portal.X && player.Y == portal.Y ||
                    player.X + 1 == portal.X && player.Y + 1 == portal.Y ||
                    player.X + 1 == portal.X && player.Y - 1 == portal.Y ||
                    player.X - 1 == portal.X && player.Y == portal.Y ||
                    player.X - 1 == portal.X && player.Y + 1 == portal.Y ||
                    player.X - 1 == portal.X && player.Y - 1 == portal.Y)
                {
                    Program.NextMap();
                }
            }
            catch
            {
                Program.SameMap();
            }
            if (tickCount == 0) gameState.BeginAct();
            foreach (var e in gameState.Animations)
                e.Location = new Point(e.Location.X + 4 * e.Command.DeltaX, e.Location.Y + 4 * e.Command.DeltaY);
            if (tickCount == 7)
            {
                gameState.EndAct();
            }
            tickCount++;
            if (tickCount == 8) tickCount = 0;
            Refresh();
        }
    }
}
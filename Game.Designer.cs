using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace DarkDivinity
{
    partial class Game : Form
    {
        private System.ComponentModel.IContainer components = null;
        private void InputKeyReader(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (MessageBox.Show("Закрыть приложение?", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    StartLogo startLogo = new StartLogo();
                    startLogo.Show();
                    Cursor.Show();
                    this.Hide();
                }
                else
                {
                    return;
                }
            }
            //MessageBox.Show(e.KeyCode.ToString(), "Pressed");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.ControlBox = false;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.ShowIcon = false;
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion
    }
}
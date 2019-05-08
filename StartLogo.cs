﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DarkDivinity
{
    public partial class StartLogo : Form
    {
        private readonly Label label;
        private readonly Button button;
        private readonly ProgressBar progressBar;
        public StartLogo()
        {
            label = new Label { Size = new Size(ClientSize.Width, 30) };
            button = new Button
            {
                Location = new Point(0, label.Bottom),
                Size = label.Size,
                Text = "Start"
            };
            progressBar = new ProgressBar
            {
                Location = new Point(0, button.Bottom),
                Size = label.Size
            };
            FormClosed += MainForm_FormClosing;
            button.Click += MakeWork;
            button.Click += OpenGameWindow;
            Controls.Add(label);
            Controls.Add(button);
            Controls.Add(progressBar);
        }

        void OpenGameWindow(object sender, EventArgs args)
        {
            Form gameForm = new Game();
            gameForm.Show();
            this.Hide();
        }

        void MainForm_FormClosing(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void MakeWork(object sender, EventArgs args)
        {
            var cancelButton = new Button
            {
                Text = "Cancel",
                Location = button.Location,
                Size = button.Size
            };

            Controls.Add(cancelButton);
            Controls.Remove(button);

            var worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += (s, a) => label.Text = "Completed";
            worker.ProgressChanged +=
                (s, progressChangedArgs) => progressBar.Value = progressChangedArgs.ProgressPercentage;

            cancelButton.Click += (s, a) => worker.CancelAsync();

            worker.RunWorkerAsync();
        }


        static void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                if (((BackgroundWorker)sender).CancellationPending) break;
                Thread.Sleep(50);
                ((BackgroundWorker)sender).ReportProgress(i);
            }
        }
    }
}
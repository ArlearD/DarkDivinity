﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DarkDivinity
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
            Cursor.Hide();
            this.KeyDown += new KeyEventHandler(InputKeyReader);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyPress
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void KeyPress1(object sender, KeyEventArgs e)
        {
            lb_keyChar.Text = "Key press: " + e.KeyValue;

        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            lb_keyChar.Text = "Key press: " + e.KeyChar;
        }
    }
}

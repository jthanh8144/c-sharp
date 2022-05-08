﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SBC
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
      
        static public ConnectDB Database;

        [STAThread]
        static void Main()
        {
            Database = new ConnectDB();
            Database.Connect();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            Database.Close();
        }
    }
}

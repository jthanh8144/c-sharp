using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LG
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        public string User
        {
            get
            {
                return txtUser.Text;
            }
            set
            {
                txtUser.Text = value;
            }
        }
        public string Pass
        {
            get
            {
                return txtPass.Text;
            }
            set
            {
                txtPass.Text = value;
            }
        }
    }
}

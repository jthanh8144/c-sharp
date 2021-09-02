using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        private void but_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void but_TT_click(object sender, EventArgs e)
        {
            if (NameBox.Text == "")
            {
                MessageBox.Show("Nhập tên khách hàng!");
            }
            else
            {
                txtTien.Text = "$" + Convert.ToString(ToTal());
            }
        }
        private double ToTal()
        {
            double s = 0;
            if (ck_CaoVoi.Checked == true) s += 100000;
            if (ck_TayTrang.Checked == true) s += 1200000;
            if (ck_ChupHinhRang.Checked == true) s += 200000;
            /*string select = comboBox1.SelectedItem.ToString();
            int r = Convert.ToInt32(select);
            s += r * 80000;*/
            s += Convert.ToInt32(comboBox1.SelectedItem.ToString()) * 80000;
            return s;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SecurityPanel
{
    public partial class Form1 : Form
    {
        private string Pass = "8144";
        public Form1()
        {
            InitializeComponent();
            btn_Accept.Focus();
            ReadFile();
        }
        private void Button_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 4)
            {
                button1.Focus();
                return;
            }
            else
            {
                textBox1.Text += ((Button)sender).Text;
            }
            btn_Accept.Focus();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        public void WriteFile(string s)
        {
            using (StreamWriter sw = File.AppendText("../../../102190190.txt"))
            {
                sw.WriteLine(DateTime.Now.ToString() + "\t" + s);
                sw.Dispose();
            }
        }

        public void ReadFile()
        {
            listBox1.Items.Clear();
            string[] s = File.ReadAllLines("../../../102190190.txt");
            foreach (string i in s)
            {
                listBox1.Items.Add(i);
            }
            listBox1.SelectedIndex = listBox1.Items.Count - 1;
        }

        private void Check_Login()
        {
            if (textBox1.Text == "") MessageBox.Show("Please enter your key!");
            else
            {
                if (textBox1.Text == Pass)
                {
                    WriteFile("Granted nl");
                    ReadFile();
                    MessageBox.Show("Success!");
                }
                else
                {
                    WriteFile("Access denied");
                    ReadFile();
                    MessageBox.Show("Failed!");
                }
                textBox1.Text = "";
            }
        }

        private void btn_Accept_Click(object sender, EventArgs e)
        {
            Check_Login();
        }

        private void Key_Press(object sender, KeyPressEventArgs e)
        {
            btn_Accept.Focus();
            if (e.KeyChar == Convert.ToChar(Keys.Back)) textBox1.Text = "";
            if (e.KeyChar == Convert.ToChar(Keys.Enter)) Check_Login();
            if (e.KeyChar >= '0' && e.KeyChar <= '9' && textBox1.Text.Length < 4)
                textBox1.Text += e.KeyChar.ToString();
        }
    }
}

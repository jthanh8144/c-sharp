using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityFrame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QLSV_conStr db = new QLSV_conStr();
            //dataGridView1.DataSource = db.SVs.ToList();
            /*var l1 = from p in db.SVs select new { p.NameSV, p.LopSH};
            dataGridView1.DataSource = l1.ToList();*/
            /*var l1 = from p in db.SVs select new { p.NameSV, p.LopSH.NameLop };
            var l2 = db.SVs.Select(p => new { p.NameSV, p.LopSH.NameLop });*/
            var l1 = from p in db.SVs where p.MSSV == "001" select new { p.NameSV, p.LopSH.NameLop };
            var l2 = db.SVs.Where(p => p.MSSV == "001").Select(p => new { p.NameSV, p.LopSH.NameLop });
            dataGridView1.DataSource = l2.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> LMSSV = new List<string>();
            foreach (string i in LMSSV)
            {
                QLSV_conStr db = new QLSV_conStr();
                SV s = db.SVs.Find(i);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            QLSV_conStr db = new QLSV_conStr();
            string MSSV = "001";
            var s = db.SVs.Where(p => p.MSSV == MSSV).FirstOrDefault();
            s.NameSV = "Ocschos";
            db.SaveChanges();
            dataGridView1.DataSource = db.SVs.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<SV> l = new List<SV>();
            QLSV_conStr db = new QLSV_conStr();
            l.Sort();
            db.SaveChanges();
            dataGridView1.DataSource = db.SVs.ToList();
        }
    }
}

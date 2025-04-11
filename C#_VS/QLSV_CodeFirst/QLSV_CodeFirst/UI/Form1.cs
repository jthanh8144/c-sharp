using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_CodeFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetCBBClass();
            SetCBBSort();
        }

        private void SetCBBClass()
        {
            Data db = new Data();
            cbb_Class.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (LopSH i in db.LopSHes)
            {
                cbb_Class.Items.Add(new CBBItem
                {
                    Value = i.ID_Lop,
                    Text = i.NameLop
                });
            }
            cbb_Class.SelectedIndex = 0;
        }

        private void SetCBBSort()
        {
            var columnnames = from t in typeof(SV).GetProperties() select t.Name;
            int i = 0;
            foreach (var col in columnnames)
            {
                cbb_Sort.Items.Add(new CBBItem
                {
                    Value = i++,
                    Text = col
                });
                if (i == 5) break;
            }
            cbb_Sort.SelectedIndex = 0;
        }

        private void ShowStudent()
        {
            var l = from p in BLL.Instance.GetListSV(((CBBItem)cbb_Class.SelectedItem).Value, tb_Search.Text)
                    select new { p.MSSV, p.NameSV, p.Gender, p.NS, p.LopSH.NameLop };
            dataGridView1.DataSource = l.ToList();
        }

        private void cbb_Class_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowStudent();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.aoe("");
            f2.reload = ShowStudent;
            f2.Show();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Select one row to edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 1)
            {
                Form2 f2 = new Form2();
                f2.aoe(r[0].Cells["MSSV"].Value.ToString());
                f2.reload = ShowStudent;
                f2.Show();
            }
            else
            {
                MessageBox.Show("Cannot edit many row!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 0)
            {
                MessageBox.Show("No rows are selected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (DataGridViewRow i in r)
            {
                BLL.Instance.DeleteStudent(i.Cells["MSSV"].Value.ToString());
            }
            ShowStudent();
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            switch (((CBBItem)cbb_Sort.SelectedItem).Value)
            {
                case 0:
                    var l = from p in BLL.Instance.GetListSV(((CBBItem)cbb_Class.SelectedItem).Value, tb_Search.Text)
                            orderby p.MSSV
                            select new { p.MSSV, p.NameSV, p.Gender, p.NS, p.LopSH.NameLop };
                    dataGridView1.DataSource = l.ToList();
                    break;
                case 1:
                    l = from p in BLL.Instance.GetListSV(((CBBItem)cbb_Class.SelectedItem).Value, tb_Search.Text)
                        orderby p.NameSV
                        select new { p.MSSV, p.NameSV, p.Gender, p.NS, p.LopSH.NameLop };
                    dataGridView1.DataSource = l.ToList();
                    break;
                case 2:
                    l = from p in BLL.Instance.GetListSV(((CBBItem)cbb_Class.SelectedItem).Value, tb_Search.Text)
                        orderby p.Gender
                        select new { p.MSSV, p.NameSV, p.Gender, p.NS, p.LopSH.NameLop };
                    dataGridView1.DataSource = l.ToList();
                    break;
                case 3:
                    l = from p in BLL.Instance.GetListSV(((CBBItem)cbb_Class.SelectedItem).Value, tb_Search.Text)
                        orderby p.NS
                        select new { p.MSSV, p.NameSV, p.Gender, p.NS, p.LopSH.NameLop };
                    dataGridView1.DataSource = l.ToList();
                    break;
                case 4:
                    l = from p in BLL.Instance.GetListSV(((CBBItem)cbb_Class.SelectedItem).Value, tb_Search.Text)
                        orderby p.ID_Lop
                        select new { p.MSSV, p.NameSV, p.Gender, p.NS, p.LopSH.NameLop };
                    dataGridView1.DataSource = l.ToList();
                    break;
            }
        }

        private void tb_Search_TextChanged(object sender, EventArgs e)
        {
            ShowStudent();
        }
    }
}

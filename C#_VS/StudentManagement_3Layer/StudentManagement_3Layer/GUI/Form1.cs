using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentManagement_3Layer.DTO;
using StudentManagement_3Layer.BLL;
using StudentManagement_3Layer.GUI;

namespace StudentManagement_3Layer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetCBBClass();
            SetCBBSort();
            ShowStudent();
            dataGridView1.Columns[0].Visible = false;
        }

        private void SetCBBClass()
        {
            cbb_Class.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (Class i in BLL_StudentManagerment.Instance.GetListClass_BLL())
            {
                cbb_Class.Items.Add(new CBBItem
                {
                    Value = i.ClassID,
                    Text = i.ClassName
                });
            }
            cbb_Class.SelectedIndex = 0;
        }

        private void SetCBBSort()
        {
            List<string> list = BLL_StudentManagerment.Instance.GetColumnName_BLL();
            for (int i = 0; i < list.Count; i++)
            {
                cbb_Sort.Items.Add(new CBBItem
                {
                    Value = i,
                    Text = list[i]
                });
            }
            cbb_Sort.SelectedIndex = 0;
        }

        private void ShowStudent()
        {
            if (((CBBItem)cbb_Class.SelectedItem).Value == 0)
            {
                dataGridView1.DataSource = BLL_StudentManagerment.Instance.ViewListStudent(0, "");
            }
            else
            {
                dataGridView1.DataSource = BLL_StudentManagerment.Instance.ViewListStudent(((CBBItem)cbb_Class.SelectedItem).Value, "");
            }
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
                f2.aoe(r[0].Cells["StudentID"].Value.ToString());
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
                BLL_StudentManagerment.Instance.DeleteStudent_BLL(i.Cells["StudentID"].Value.ToString());
            }
            ShowStudent();
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            List<string> ListID = new List<string>();
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                ListID.Add(dataGridView1.Rows[i].Cells["StudentID"].Value.ToString());
            }
            dataGridView1.DataSource = BLL_StudentManagerment.Instance.ListStudentSort
                (BLL_StudentManagerment.Instance.GetListStudentDgv(ListID), ((CBBItem)cbb_Sort.SelectedItem).Value);
        }

        private void tb_Search_TextChanged(object sender, EventArgs e)
        {
            if (tb_Search.Text == "") ShowStudent();
            else
            {
                dataGridView1.DataSource = BLL_StudentManagerment.Instance.ViewListStudent(((CBBItem)cbb_Class.SelectedItem).Value,
                    tb_Search.Text);
            }
        }
    }
}

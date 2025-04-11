using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_3Layer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SetCBBClass();
            setCBBSort();
        }

        public void SetCBBClass()
        {
            cbb_Class.Items.Add(new CBBItem { Value = 0, Text = "All" });
            foreach (Class i in CSDL_OOP.Instance.ListClass)
            {
                cbb_Class.Items.Add(new CBBItem
                {
                    Value = Convert.ToInt32(i.ClassID.ToString()),
                    Text = i.ClassName
                });
            }
            cbb_Class.SelectedIndex = 0;
        }

        public void setCBBSort()
        {
            for (int i = 0; i < CSDL_OOP.Instance.listName.Length; i++)
            {
                cbb_Sort.Items.Add(new CBBItem
                {
                    Text = CSDL_OOP.Instance.listName[i],
                    Value = i
                });
            }
            cbb_Sort.SelectedIndex = 0;
        }

        public void ShowStudent()
        {
            if (((CBBItem)cbb_Class.SelectedItem).Value == 0)
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.ListStudent;
            }
            else
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.getStudent(((CBBItem)cbb_Class.SelectedItem).Value, "");
            }
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            ShowStudent();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Please select an option to edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 1)
            {
                Form2 f2 = new Form2();
                DataGridViewRow dr = dataGridView1.CurrentRow;
                f2.d(CSDL_OOP.Instance.IndexOf(dr.Cells["StudentID"].Value.ToString()));
                f2.ReLoad = ShowStudent;
                f2.Show();
            }
            else
            {
                MessageBox.Show("Please select one row to edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.d(-1);
            f2.ReLoad = ShowStudent;
            f2.Show();
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to sort!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            dataGridView1.DataSource = null;
            int propertyIndex = ((CBBItem)cbb_Sort.SelectedItem).Value;
            List<Student> list = new List<Student>();
            if (((CBBItem)cbb_Class.SelectedItem).Value == 0)
            {
                list = CSDL_OOP.Instance.ListStudent;
            }
            else
            {
                list = CSDL_OOP.Instance.getStudent(((CBBItem)cbb_Class.SelectedItem).Value, "");
            }
            dataGridView1.DataSource = CSDL_OOP.Instance.sortListStudent(list, propertyIndex);
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
                CSDL_OOP.Instance.DeleteStudentInCSDL(CSDL_OOP.Instance.IndexOf(i.Cells["StudentID"].Value.ToString()));
            }
            ShowStudent();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            if (s == "")
            {
                MessageBox.Show("The search box is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (((CBBItem)cbb_Class.SelectedItem).Value == 0)
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.getStudent(s);
            }
            else
            {
                dataGridView1.DataSource = CSDL_OOP.Instance.getStudent(((CBBItem)cbb_Class.SelectedItem).Value, s);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Focus();
            setCBBClass();
            setCBBSort();
        }

        public void setCBBClass()
        {
            cbb_Class.Items.Add("All");
            DataTable dt = CSDL.Instance.DTLSH;
            foreach (DataRow dr in dt.Rows)
            {
                cbb_Class.Items.Add(dr["ClassName"]);
            }
        }

        public void setCBBSort()
        {
            DataTable dt = CSDL.Instance.DTSV;
            foreach (DataColumn dc in dt.Columns)
            {
                cbb_Sort.Items.Add(dc.ColumnName);
            }
        }

        private void btn_Show_Click(object sender, EventArgs e)
        {
            int index = cbb_Class.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Please select an option to display!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (index == 0)
            {
                dataGridView1.DataSource = CSDL.Instance.DTSV;
                return;
            }
            CBBItem cbb = new CBBItem()
            {
                Text = cbb_Class.Items[index - 1].ToString(),
                Value = Convert.ToInt32(CSDL.Instance.DTLSH.Rows[index - 1]["ClassID"])
            };
            dataGridView1.DataSource = CSDL.Instance.createDataTableByID(cbb.Value);
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            int index = cbb_Class.SelectedIndex;
            string s = textBox1.Text;
            if (index == -1)
            {
                MessageBox.Show("Please select an option to display!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (index == 0)
            {
                dataGridView1.DataSource = CSDL.Instance.createDataTableByName(s);
                return;
            }
            if (s == "")
            {
                MessageBox.Show("The search box is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            CBBItem cbb = new CBBItem()
            {
                Text = cbb_Class.Items[index - 1].ToString(),
                Value = Convert.ToInt32(CSDL.Instance.DTLSH.Rows[index - 1]["ClassID"])
            };
            dataGridView1.DataSource = CSDL.Instance.createDataTable(cbb.Value, s);
        }

        private void btn_Sort_Click(object sender, EventArgs e)
        {
            if (cbb_Sort.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an option for arrangement!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataTable temp = new DataTable();
            CSDL.Instance.Sort(temp, cbb_Sort.Text, cbb_Class.Text);
            dataGridView1.DataSource = temp;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.d(0, "add");
            f2.Show();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Please select an option to edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DataGridViewRow dr = dataGridView1.CurrentRow;
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            if (r.Count == 1)
            {
                Form2 f2 = new Form2();
                f2.d(CSDL.Instance.IndexOf(dr.Cells["StudentID"].Value.ToString()), "edit");
                f2.Show();
            }
            else
            {
                MessageBox.Show("Please select one row to edit!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                CSDL.Instance.Delete(i.Cells["StudentID"].Value.ToString());
            }
            btn_Show_Click(sender, e);
        }
    }
}

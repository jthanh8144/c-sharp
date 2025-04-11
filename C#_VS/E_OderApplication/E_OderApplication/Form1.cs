using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_OderApplication
{
    public partial class Form1 : Form
    {
        public DataTable[] ListTable { get; set; }
        public int LastSelectedIndex { get; set; }
        public Form1()
        {
            InitializeComponent();
            InitTable();
            LastSelectedIndex = -1;
            comboBox1.SelectedIndex = 0;
        }

        private void InitTable()
        {
            ListTable = new DataTable[comboBox1.Items.Count];
            for (int i = 0; i < ListTable.Length; i++)
            {
                ListTable[i] = new DataTable();
                ListTable[i].Columns.Add("Món ăn", typeof(string));
                ListTable[i].Columns.Add("Số lượng", typeof(string));
            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            string select = ((Button)sender).Text;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                if (dataGridView1[0, i].Value.ToString() == select)
                {
                    dataGridView1[1, i].Value = (Convert.ToInt32(dataGridView1[1, i].Value) + 1).ToString();
                    return;
                }
            }
            string[] temp = new string[] { select, "1" };
            dataGridView1.Rows.Add(temp);
        }

        private void Btn_Order_Click(object sender, EventArgs e)
        {
            int index = comboBox1.SelectedIndex;
            int count = 0;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (count++ == dataGridView1.Rows.Count - 1) break;
                DataRow row = ListTable[index].NewRow();
                foreach (DataGridViewCell c in r.Cells)
                {
                    row[c.ColumnIndex] = c.Value;
                }
                ListTable[index].Rows.Add(row);
            }
            if (ListTable[index].Rows.Count >= 1) 
                MessageBox.Show("Order thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else 
                MessageBox.Show("Bàn chưa có order nào cả!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void QuestionChangeTable()
        {
            DialogResult d = MessageBox.Show("Bạn có muốn lưu order trước khi thay đổi bàn không?", "Unsaved order",
                   MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            switch (d)
            {
                case DialogResult.Yes:
                    int index = LastSelectedIndex;
                    ListTable[index] = new DataTable();
                    ListTable[index].Columns.Add("Món ăn", typeof(string));
                    ListTable[index].Columns.Add("Số lượng", typeof(string));
                    int count = 0;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        if (count++ == dataGridView1.Rows.Count - 1) break;
                        DataRow row = ListTable[index].NewRow();
                        foreach (DataGridViewCell c in r.Cells)
                        {
                            row[c.ColumnIndex] = c.Value;
                        }
                        ListTable[index].Rows.Add(row);
                    }
                    if (ListTable[index].Rows.Count > 1)
                        MessageBox.Show("Order thành công!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void ViewTable()
        {
            dataGridView1.Rows.Clear();
            foreach (DataRow row in ListTable[comboBox1.SelectedIndex].Rows)
            {
                string[] r = row.ItemArray.OfType<string>().ToArray();
                dataGridView1.Rows.Add(r);
            }
            dataGridView1.Refresh();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (LastSelectedIndex == -1 || (dataGridView1.Rows.Count == 1 && ListTable[LastSelectedIndex].Rows.Count == 0))
            {
                LastSelectedIndex = comboBox1.SelectedIndex;
                ViewTable();
                return;
            }
            else
            {
                if (ListTable[comboBox1.SelectedIndex].Rows.Count >= 0)
                {
                    if (dataGridView1.Rows.Count - 1 != ListTable[LastSelectedIndex].Rows.Count)
                    {
                        QuestionChangeTable();
                    }
                    else
                    {
                        if (dataGridView1.Rows.Count - 1 != 0)
                            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                            {
                                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                                {
                                    if (dataGridView1[j, i].Value != ListTable[LastSelectedIndex].Rows[i][j])
                                        QuestionChangeTable();
                                }
                            }
                    }
                    ViewTable();
                }
            }
            LastSelectedIndex = comboBox1.SelectedIndex;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            int i = comboBox1.SelectedIndex;
            ListTable[i] = new DataTable();
            ListTable[i].Columns.Add("Món ăn", typeof(string));
            ListTable[i].Columns.Add("Số lượng", typeof(string));
            ViewTable();
        }
    }
}

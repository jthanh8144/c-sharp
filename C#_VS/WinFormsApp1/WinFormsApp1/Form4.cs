using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            CreateCol();
            CreateRow();
            View();
        }
        public void CreateCol()
        {
            ColumnHeader c1 = new ColumnHeader();
            c1.Text = "MSV";
            ColumnHeader c2 = new ColumnHeader();
            c2.Text = "NameSV";
            ColumnHeader c3 = new ColumnHeader();
            c3.Text = "Class";
            listView1.Columns.AddRange(new ColumnHeader[] { c1, c2, c3 });
        }

        public void CreateRow()
        {
            ListViewItem i1 = new ListViewItem();
            i1.Text = "102190190";
            ListViewItem.ListViewSubItem sub1_1 = new ListViewItem.ListViewSubItem();
            sub1_1.Text = "NVA";
            ListViewItem.ListViewSubItem sub1_2 = new ListViewItem.ListViewSubItem();
            sub1_2.Text = "19DT4";
            i1.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { sub1_1, sub1_2 });
            listView1.Items.Add(i1);

            ListViewItem i2 = new ListViewItem();
            i2.Text = "102190191";
            ListViewItem.ListViewSubItem sub2_1 = new ListViewItem.ListViewSubItem();
            sub2_1.Text = "NVB";
            ListViewItem.ListViewSubItem sub2_2 = new ListViewItem.ListViewSubItem();
            sub2_2.Text = "19DT4";
            i2.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { sub2_1, sub2_2 });
            listView1.Items.Add(i2);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection r = listView1.SelectedItems;
            string s = "";
            foreach (ListViewItem i in r)
            {
                s += "MSV: " + i.Text + ", Name: " + i.SubItems[1].Text + ", Class: " + i.SubItems[2].Text + "\n";
            }
            MessageBox.Show(s);
        }

        public void View()
        {
            SV[] arr = new SV[]
            {
                new SV {MSV = "001", NameSV = "NVA", Gender = true },
                new SV {MSV = "002", NameSV = "NVB", Gender = true },
                new SV {MSV = "003", NameSV = "NVC", Gender = false }
            };
            //dataGridView1.DataSource = arr;

            List<SV> l = new List<SV>();
            l.AddRange(new SV[]
            {
                new SV {MSV = "001", NameSV = "NVA", Gender = true },
                new SV {MSV = "002", NameSV = "NVB", Gender = true },
                new SV {MSV = "003", NameSV = "NVC", Gender = false }
            });
            //dataGridView1.DataSource = l;

            DataTable dt = new DataTable();
            dt.Columns.Add("MSV", typeof(string));
            dt.Columns.Add("NameSv", typeof(string));
            dt.Columns.Add("Gender", typeof(bool));
            DataRow dr = dt.NewRow();
            dr[0] = "001";
            dr[1] = "NVA";
            dr[2] = true;
            DataRow dr1 = dt.NewRow();
            dr1[0] = "002";
            dr1[1] = "NVB";
            dr1[2] = false;
            dt.Rows.Add(dr);
            dt.Rows.Add(dr1);
            dataGridView1.DataSource = dt;
        }
        
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1.CurrentCell.Value.ToString());
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            /*DataGridViewRow r = dataGridView1.CurrentRow;
            string s = "";
            s += r.Cells["Msv"].Value.ToString() + ", "
                + r.Cells["Namesv"].Value.ToString() + ", "
                + r.Cells["Gender"].Value.ToString();
            MessageBox.Show(s);*/
            DataGridViewSelectedRowCollection r = dataGridView1.SelectedRows;
            string s = "";
            foreach (DataGridViewRow i in r)
            {
                s += i.Cells["MSV"].Value.ToString() + ", "
                    + i.Cells["NameSV"].Value.ToString() + ", "
                    + i.Cells["Gender"].Value.ToString() + "\n";
            }
            MessageBox.Show(s);
        }

    } 
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Review1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            loadDataSource();
        }

        public void loadDataSource()
        {
            foreach (SinhVien s in CSDL_OOP.Instance.ArrayList)
            {
                DataGridViewRow dr = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                dr.Cells[0].Value = s.MSV;
                dr.Cells[1].Value = s.HoTen;
                dr.Cells[2].Value = s.NgaySinh;
                dr.Cells[3].Value = s.DiaChi;
                dr.Cells[4].Value = s.SDT;
                dr.Cells[5].Value = s.NienKhoa;
                dr.Cells[6].Value = s.LoaiHinh();
                dataGridView1.Rows.Add(dr);
            }
        }
    }
}

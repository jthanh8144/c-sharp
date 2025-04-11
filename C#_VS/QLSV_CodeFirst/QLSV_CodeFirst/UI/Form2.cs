using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_CodeFirst
{
    public partial class Form2 : Form
    {
        public delegate void AddOrEdit(string StudentID);
        public AddOrEdit aoe;
        public delegate void ReLoadData();
        public ReLoadData reload;

        private static string StudentID = "";
        private static bool status = true;

        public void getIndexEdit(string StudentID)
        {
            Form2.StudentID = StudentID;
        }

        public Form2()
        {
            aoe = getIndexEdit;
            InitializeComponent();
            SetCBBClass();
        }

        private void SetCBBClass()
        {
            Data db = new Data();
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

        private SV getStudentInfo()
        {
            SV s = new SV();
            s.MSSV = tb_StudentID.Text;
            s.NameSV = tb_Name.Text;
            if (rbtn_Male.Checked == true)
            {
                s.Gender = true;
            }
            else
            {
                s.Gender = false;
            }
            s.NS = dateTimePicker1.Value;
            s.ID_Lop = ((CBBItem)cbb_Class.SelectedItem).Value;
            return s;
        }

        private void AddStudent()
        {
            Data db = new Data();
            if (tb_StudentID.Text == "")
            {
                MessageBox.Show("Please enter their student ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
            }
            foreach (SV i in db.SVs)
            {
                if (tb_StudentID.Text == i.MSSV.ToString())
                {
                    MessageBox.Show("Student ID is exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = false;
                    return;
                }
            }
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Invalid time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
            }
            BLL.Instance.AddStudent(getStudentInfo());
            status = true;
        }

        private void EditStudent()
        {
            if (dateTimePicker1.Value > DateTime.Now)
            {
                MessageBox.Show("Invalid time!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
            }
            BLL.Instance.EditStudent(getStudentInfo());
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (StudentID == "")
            {
                AddStudent();
            }
            else
            {
                EditStudent();
            }
            if (status == true)
            {
                reload();
                Dispose();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (StudentID != "")
            {
                tb_StudentID.Enabled = false;
                SV s = BLL.Instance.GetSVByMSSV(StudentID);
                tb_StudentID.Text = s.MSSV;
                tb_Name.Text = s.NameSV;
                for (int i = 0; i < cbb_Class.Items.Count; i++)
                {
                    if (s.ID_Lop == ((CBBItem)cbb_Class.Items[i]).Value)
                    {
                        cbb_Class.SelectedIndex = i;
                        break;
                    }
                }
                dateTimePicker1.Value = s.NS;
                if (s.Gender == true)
                {
                    rbtn_Male.Checked = true;
                }
                else
                {
                    rbtn_Female.Checked = true;
                }
            }
        }
    }
}

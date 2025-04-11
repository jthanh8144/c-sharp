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
    public partial class Form2 : Form
    {
        public delegate void MyDel(int index);
        public MyDel d;
        public delegate void ReloadData();
        public ReloadData ReLoad;
        static int index = 0;
        static bool status = true;

        public Form2()
        {
            d = new MyDel(getIndexEditStudent);
            InitializeComponent();
            SetCBBClass();
        }

        public void SetCBBClass()
        {
            foreach (DataRow i in CSDL.Instance.DTLSH.Rows)
            {
                cbb_Class.Items.Add(new CBBItem
                {
                    Value = Convert.ToInt32(i["ClassID"].ToString()),
                    Text = i["ClassName"].ToString()
                });
            }
        }

        public void getIndexEditStudent(int index)
        {
            Form2.index = index;
        }

        public void AddStudent()
        {            
            if (tb_StudentID.Text == "")
            {
                MessageBox.Show("Please enter their student ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
            }
            foreach (DataRow i in CSDL.Instance.DTSV.Rows)
            {
                if (tb_StudentID.Text == i["StudentID"].ToString())
                {
                    MessageBox.Show("Student ID is exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = false;
                    return;
                }
            }
            if (rbtn_Male.Checked == false && rbtn_Female.Checked == false)
            {
                MessageBox.Show("Please choose their gender!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
            }
            if (cbb_Class.SelectedIndex == -1)
            {
                MessageBox.Show("Please choose their class!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
            }
            Student s = new Student();
            s.StudentID = tb_StudentID.Text;
            s.Name = tb_Name.Text;
            if (rbtn_Male.Checked == true)
            {
                s.Gender = true;
            }
            else
            {
                s.Gender = false;
            }
            s.DateOfBirth = dateTimePicker1.Value;
            s.ClassID = ((CBBItem)cbb_Class.SelectedItem).Value;
            CSDL_OOP.Instance.AddStudentToCSDL(s);
            status = true;
        }

        public void EditStudent()
        {
            Student s = new Student();
            s.StudentID = tb_StudentID.Text;
            s.Name = tb_Name.Text;
            if (rbtn_Male.Checked == true)
            {
                s.Gender = true;
            }
            else
            {
                s.Gender = false;
            }
            s.DateOfBirth = dateTimePicker1.Value;
            s.ClassID = ((CBBItem)cbb_Class.SelectedItem).Value;
            CSDL_OOP.Instance.EditStudentInCSDL(s, index);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (index == -1)
            {
                AddStudent();
            }
            else
            {
                EditStudent();
            }
            if (status == true)
            {
                ReLoad();
                Dispose();
            }
        }

        public void LoadDataToEdit()
        {
            Student s = CSDL_OOP.Instance.ListStudent[index];
            tb_StudentID.Text = s.StudentID;
            tb_Name.Text = s.Name;
            for (int i = 0; i < cbb_Class.Items.Count; i++)
            {
                if (s.ClassID == ((CBBItem)cbb_Class.Items[i]).Value)
                {
                    cbb_Class.SelectedIndex = i;
                    break;
                }
            }
            dateTimePicker1.Value = s.DateOfBirth;
            if (s.Gender == true)
            {
                rbtn_Male.Checked = true;
            }
            else
            {
                rbtn_Female.Checked = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (index != -1)
            {
                tb_StudentID.Enabled = false;
                LoadDataToEdit();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

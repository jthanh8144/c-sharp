using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StudentManagement_3Layer.DAL;
using StudentManagement_3Layer.DTO;
using StudentManagement_3Layer.BLL;

namespace StudentManagement_3Layer.GUI
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

        private Student getStudentInfo()
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
            return s;
        }

        private void AddStudent()
        {
            if (tb_StudentID.Text == "")
            {
                MessageBox.Show("Please enter their student ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
            }
            foreach (Student i in BLL_StudentManagerment.Instance.GetListStudent_BLL())
            {
                if (tb_StudentID.Text == i.StudentID.ToString())
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
            BLL_StudentManagerment.Instance.AddStudent_BLL(getStudentInfo());
            status = true;
        }

        private void EditStudent()
        {
            BLL_StudentManagerment.Instance.EditStudent_BLL(getStudentInfo());
            status = true;
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
                Student s = BLL_StudentManagerment.Instance.GetStudentByID_BLL(StudentID);
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
        }
    }
}

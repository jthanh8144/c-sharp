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
    public partial class Form2 : Form
    {
        public delegate void MyDel(int index, string option);
        public MyDel d;
        static int index = 0;
        static string option = "";
        static bool status = true;
        public void getIndexEditStudent(int index, string option)
        {
            Form2.index = index;
            Form2.option = option;
        }
        public Form2()
        {
            d = new MyDel(getIndexEditStudent);
            InitializeComponent();
            setCBBClass();
        }

        public void setCBBClass()
        {
            DataTable dt = CSDL.Instance.DTLSH;
            foreach (DataRow dr in dt.Rows)
            {
                cbb_Class.Items.Add(dr["ClassName"]);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadDataToEdit()
        {
            DataRow dr = CSDL.Instance.DTSV.Rows[index];
            tb_StudentID.Text = dr["StudentID"].ToString();
            tb_Name.Text = dr["Name"].ToString();
            for (int i = 0; i < cbb_Class.Items.Count; i++)
            {
                if (cbb_Class.Items[i].ToString() == CSDL.Instance.getClassName(Convert.ToInt32(dr["ClassID"])))
                {
                    cbb_Class.SelectedIndex = i;
                    break;
                }
            }
            dateTimePicker1.Value = Convert.ToDateTime(dr["DateOfBirth"]);
            if (Convert.ToBoolean(dr["Gender"]) == true) rbtn_Male.Checked = true;
            else rbtn_Female.Checked = true;
            tb_StudentID.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (option == "edit") LoadDataToEdit();
        }

        private void AddStudent()
        {
            if (tb_StudentID.Text == "")
            {
                MessageBox.Show("Please enter their student ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                status = false;
                return;
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
            foreach (DataRow i in CSDL.Instance.DTSV.Rows)
            {
                if (tb_StudentID.Text == i["StudentID"].ToString())
                {
                    MessageBox.Show("Student ID is exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    status = false;
                    return;
                }
            }
            DataRow dr = CSDL.Instance.DTSV.NewRow();
            dr["StudentID"] = tb_StudentID.Text;
            dr["Name"] = tb_Name.Text;
            if (rbtn_Male.Checked == true) dr["Gender"] = true;
            else dr["Gender"] = false;
            dr["DateOfBirth"] = dateTimePicker1.Value;
            dr["ClassID"] = CSDL.Instance.getClassId(cbb_Class.Text);
            CSDL.Instance.DTSV.Rows.Add(dr);
        }

        private void EditStudent()
        {
            object[] o = new object[CSDL.Instance.DTSV.Columns.Count];
            o[0] = tb_StudentID.Text;
            o[1] = tb_Name.Text;
            if (rbtn_Male.Checked == true) o[2] = true;
            else o[2] = false;
            o[3] = dateTimePicker1.Value;
            o[4] = CSDL.Instance.getClassId(cbb_Class.Text);
            CSDL.Instance.Update_Student(o, index);
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            switch (option)
            {
                case "add":
                    AddStudent();
                    break;
                case "edit":
                    EditStudent();
                    break;
            }
            if (status == true) Close();
        }
    }
}

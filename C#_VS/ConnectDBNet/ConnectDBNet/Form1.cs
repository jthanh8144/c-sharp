using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectDBNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*string cnnStr = @"Data Source=ACERNITRO5;Initial Catalog=Net;User ID=sa;Password=123456";
            SqlConnection cnn = new SqlConnection(cnnStr);
            SqlCommand cmd = new SqlCommand(textBox1.Text, cnn);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();*/
            //ShowDS();
            string query = "select * from Student";
            dataGridView1.DataSource = DBHelper.Instance.GetRecord(query);
        }
        private void Show()
        {
            string cnnStr = @"Data Source=ACERNITRO5;Initial Catalog=Net;User ID=sa;Password=123456";
            SqlConnection cnn = new SqlConnection(cnnStr);
            string query = "select * from Student";
            SqlCommand cmd = new SqlCommand(query, cnn);
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("StudentID", typeof(string)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Gender", typeof(bool)),
                new DataColumn("DateOfBirth", typeof(DateTime)),
                new DataColumn("ClassID", typeof(int))
            });
            cnn.Open();
            //textBox1.Text = ((int)cmd.ExecuteScalar()).ToString();
            //textBox1.Text = cnn.State.ToString();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                DataRow dr = dt.NewRow();
                dr["StudentID"] = r["StudentID"];
                dr["Name"] = r["Name"];
                dr["Gender"] = r["Gender"];
                dr["DateOfBirth"] = r["DateOfBirth"];
                dr["ClassID"] = r["ClassID"];
                dt.Rows.Add(dr);
            }
            cnn.Close();
            dataGridView1.DataSource = dt;
            //textBox1.Text = cnn.State.ToString();
        }

        private void ShowDS()
        {
            string cnnStr = @"Data Source=ACERNITRO5;Initial Catalog=Net;User ID=sa;Password=123456";
            SqlConnection cnn = new SqlConnection(cnnStr);
            string query = "select * from Student";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            cnn.Open();
            da.Fill(dt);
            cnn.Close();
            dataGridView1.DataSource = dt;
        }
    }
}

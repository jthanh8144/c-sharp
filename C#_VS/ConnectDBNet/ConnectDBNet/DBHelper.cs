using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectDBNet
{
    class DBHelper
    {
        private static SqlConnection cnn;
        public static DBHelper Instance
        {
            get
            { 
                if (_Instance == null)
                {
                    string cnnStr = @"Data Source=ACERNITRO5;Initial Catalog=Net;User ID=sa;Password=123456";
                    _Instance = new DBHelper(cnnStr);
                }
                return _Instance;
            }
            private set
            {
            }
        }
        private static DBHelper _Instance;

        private DBHelper(string s)
        {
            cnn = new SqlConnection(s);
        }

        public DataTable GetRecord(string s)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(s, cnn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cnn.Open();
            da.Fill(dt);
            cnn.Close();
            return dt;
        }

        public void ExcuteDB(string s)
        {
            SqlCommand cmd = new SqlCommand(s, cnn);
            cnn.Open();
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}

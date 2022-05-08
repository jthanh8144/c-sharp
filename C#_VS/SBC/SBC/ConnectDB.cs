using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBC
{ class ConnectDB
    {
        OleDbConnection myConnection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Book.accdb");

        public void Connect()
        {

            try
            {
                myConnection.Open();
            }
            catch {
                
            }

        }

        // Hàm tạo query tới database để lấy dữ liệu 
        public DataTable Query(String sql)
        {
            // Tạo kết nối tới database
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, myConnection);
            // Tạo bảng để chứa dữ liệu
            DataTable data = new DataTable();
            // Điền dữ liệu đọc được từ databse vào bảng đã tạo
            adapter.Fill(data);
            // Trả về bảng đã fill dữ liệu
            return data;
        }

        // Hàm tạo command để thực thi lệnh không trả về data như insert, update, delete
        public void NonQuery(String sql)
        {
            // Tạo command
            OleDbCommand cmd = myConnection.CreateCommand();
            // Gán lệnh cho command
            cmd.CommandText = sql;
            // Thực thi lệnh
            cmd.ExecuteNonQuery();
        }

        // Hàm lấy số lượng sách theo mã sách trong database
        public int GetNumberOfBook(String maSach)
        {
            // Tạo query
            String sql = "SELECT Soluong FROM Sach WHERE Masach = '" + maSach + "'";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, myConnection);
            DataTable data = new DataTable();
            adapter.Fill(data);
            // Trả về số lượng sách theo mã sách có trong database
            return Int32.Parse(data.Rows[0][0].ToString());
        }

        public void Close()
        {
            try
            {
                if (myConnection != null && myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            }
            catch { }
        }
    }
}


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV
{
    class CSDL
    {
        public DataTable DTSV { get; set; }
        public DataTable DTLSH { get; set; }
        public static CSDL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CSDL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        public static CSDL _Instance;
        private CSDL()
        {
            DTSV = new DataTable();
            DTSV.Columns.AddRange(new DataColumn[]
            {
                new DataColumn( "StudentID", typeof(string)),
                new DataColumn( "Name", typeof(string)),
                new DataColumn( "Gender", typeof(bool)),
                new DataColumn( "DateOfBirth", typeof(DateTime)),
                new DataColumn( "ClassID", typeof(int))
            });
            DataRow dr = DTSV.NewRow();
            dr["StudentID"] = "101";
            dr["Name"] = "NVA";
            dr["Gender"] = true;
            dr["DateOfBirth"] = DateTime.Now;
            dr["ClassID"] = 1;
            DTSV.Rows.Add(dr);

            DataRow dr1 = DTSV.NewRow();
            dr1["StudentID"] = "102";
            dr1["Name"] = "NVB";
            dr1["Gender"] = false;
            dr1["DateOfBirth"] = DateTime.Now;
            dr1["ClassID"] = 1;
            DTSV.Rows.Add(dr1);

            DataRow dr2= DTSV.NewRow();
            dr2["StudentID"] = "103";
            dr2["Name"] = "NVC";
            dr2["Gender"] = true;
            dr2["DateOfBirth"] = DateTime.Now;
            dr2["ClassID"] = 2;
            DTSV.Rows.Add(dr2);

            DataRow dr6 = DTSV.NewRow();
            dr6["StudentID"] = "106";
            dr6["Name"] = "NVC";
            dr6["Gender"] = true;
            dr6["DateOfBirth"] = DateTime.Now;
            dr6["ClassID"] = 1;
            DTSV.Rows.Add(dr6);

            DataRow dr7 = DTSV.NewRow();
            dr7["StudentID"] = "107";
            dr7["Name"] = "NVD";
            dr7["Gender"] = false;
            dr7["DateOfBirth"] = DateTime.Now;
            dr7["ClassID"] = 1;
            DTSV.Rows.Add(dr7);

            DTLSH = new DataTable();
            DTLSH.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ClassID", typeof(int)),
                new DataColumn("ClassName", typeof(string)),
            });
            DataRow dr3 = DTLSH.NewRow();
            dr3["ClassID"] = 1;
            dr3["ClassName"] = "LSH1";
            DTLSH.Rows.Add(dr3);
            DataRow dr4 = DTLSH.NewRow();
            dr4["ClassID"] = 2;
            dr4["ClassName"] = "LSH2";
            DTLSH.Rows.Add(dr4);
        }
        public DataTable createDataTableByID(int ClassID)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("StudentID"),
                new DataColumn("Name"),
                new DataColumn("Gender", typeof(bool)),
                new DataColumn("Date Of Birth", typeof(DateTime)),
                new DataColumn("ClassID", typeof(int))
            });
            foreach (DataRow dr in DTSV.Rows)
            {
                if (Convert.ToInt32(dr["ClassID"]) == ClassID) dt.Rows.Add(dr.ItemArray);
            }
            return dt;
        }

        public DataTable createDataTableByName(string StudentName)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("StudentID"),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Gender", typeof(bool)),
                new DataColumn("DateOfBirth", typeof(DateTime)),
                new DataColumn("ClassID", typeof(int))
            });
            foreach (DataRow dr in DTSV.Rows)
            {
                if (dr["Name"].ToString() == StudentName) dt.Rows.Add(dr.ItemArray);
            }
            return dt;
        }

        public DataTable createDataTable(int ClassID, string StudentName)
        {
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("StudentID"),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Gender", typeof(bool)),
                new DataColumn("DateOfBirth", typeof(DateTime)),
                new DataColumn("ClassID", typeof(int))
            });
            foreach (DataRow dr in DTSV.Rows)
            {
                if (dr["Name"].ToString() == StudentName && Convert.ToInt32(dr["ClassID"]) == ClassID) dt.Rows.Add(dr.ItemArray);
            }
            return dt;
        }

        public int getClassId(string ClassName)
        {
            foreach (DataRow dr in DTLSH.Rows)
            {
                if (dr["ClassName"].ToString() == ClassName) return Convert.ToInt32(dr["ClassID"].ToString());
            }
            return 0;
        }

        public string getClassName(int ClassID)
        {
            foreach (DataRow dr in DTLSH.Rows)
            {
                if (Convert.ToInt32(dr["ClassID"]) == ClassID) return dr["ClassName"].ToString();
            }
            return "";
        }

        public int IndexOf(string StudentID)
        {
            for (int i = 0; i < DTSV.Rows.Count; i++)
            {
                if (DTSV.Rows[i]["StudentID"].ToString() == StudentID) return i;
            }
            return -1;
        }

        public void Delete(string StudentID)
        {
            if (IndexOf(StudentID) == -1)
            {
                MessageBox.Show("Không tìm thấy mã sinh viên tương ứng!");
                return;
            }
            else
            {
                DTSV.Rows.Remove(DTSV.Rows[IndexOf(StudentID)]);
            }
        }

        public void Update_Student(object[] o, int index)
        {
            DTSV.Rows[index].ItemArray = o;
        }

        public void Sort(DataTable temp, string Value, string list)
        {
            int indexCol = -1;
            temp.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("StudentID", typeof(string)),
                new DataColumn("Name", typeof(string)),
                new DataColumn("Gender", typeof(bool)),
                new DataColumn("DateOfBirth", typeof(DateTime)),
                new DataColumn("ClassID", typeof(int))
            });
            if (list == "All")
            {
                foreach (DataRow i in DTSV.Rows)
                {
                    temp.Rows.Add(i.ItemArray);
                }
            }
            else
            {
                foreach (DataRow i in createDataTableByID(getClassId(list)).Rows)
                {
                    temp.Rows.Add(i.ItemArray);
                }
            }
            for (int i = 0; i < temp.Columns.Count; i++)
            {
                if (temp.Columns[i].ColumnName == Value)
                {
                    indexCol = i;
                    break;
                }
            }
            for (int i = 0; i < temp.Rows.Count; i++)
            {
                for (int j = i + 1; j < temp.Rows.Count; j++)
                {
                    if (String.Compare(temp.Rows[i][indexCol].ToString(), temp.Rows[j][indexCol].ToString()) > 0)
                    {
                        object[] t = temp.Rows[i].ItemArray;
                        temp.Rows[i].ItemArray = temp.Rows[j].ItemArray;
                        temp.Rows[j].ItemArray = t;
                    }
                }
            }
        }
    }
}

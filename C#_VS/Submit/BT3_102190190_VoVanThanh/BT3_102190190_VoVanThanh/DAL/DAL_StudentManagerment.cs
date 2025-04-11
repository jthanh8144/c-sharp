using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BT3_102190190_VoVanThanh.DTO;
using BT3_102190190_VoVanThanh.DAL;
using System.Windows.Forms;

namespace BT3_102190190_VoVanThanh.DAL
{
    class DAL_StudentManagerment
    {
        public static DAL_StudentManagerment Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new DAL_StudentManagerment();
                }
                return _Instance;
            }
            private set
            {
            }
        }
        private static DAL_StudentManagerment _Instance;

        public Student Get1Student(DataRow dr)
        {
            Student s = new Student();
            s.StudentID = dr["MSSV"].ToString();
            s.Name = dr["NameSV"].ToString();
            s.Gender = Convert.ToBoolean(dr["Gender"]);
            s.DateOfBirth = Convert.ToDateTime(dr["NS"]);
            s.ClassID = Convert.ToInt32(dr["ID_Lop"]);
            return s;
        }

        public List<Student> GetListStudent_DAL()
        {
            List<Student> data = new List<Student>();
            string query = "select * from SV";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(Get1Student(i));
            }
            return data;
        }

        public List<Student> GetListStudent_DAL(int ClassID, string Name)
        {
            List<Student> data = new List<Student>();
            string query;
            if (ClassID == 0 && Name != "")
            {
                foreach (Student i in GetListStudent_DAL())
                {
                    if (i.Name.ToUpper().Contains(Name.ToUpper()))
                    {
                        data.Add(i);
                    }
                }
            }
            else
            {
                query = "select * from SV where ID_Lop = " + ClassID.ToString();
                if (Name == "")
                {
                    foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
                    {
                        data.Add(Get1Student(i));
                    }
                }
                else
                {
                    foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
                    {
                        if (i["NameSV"].ToString().ToUpper().Contains(Name.ToUpper()))
                        {
                            data.Add(Get1Student(i));
                        }
                    }
                }
            }
            return data;
        }

        public Class Get1Class(DataRow dr)
        {
            Class s = new Class();
            s.ClassID = Convert.ToInt32(dr["ID_Lop"]);
            s.ClassName = dr["NameLop"].ToString();
            return s;
        }

        public List<Class> GetListClass_DAL()
        {
            List<Class> data = new List<Class>();
            string query = "select * from LopSH";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                data.Add(Get1Class(i));
            }
            return data;
        }

        public List<string> GetColumnName_DAL()
        {
            List<string> list = new List<string>();
            string query = "select sys.all_columns.name from sys.all_columns where object_id = " +
                "(select object_id from sys.objects where type='u' and name ='SV')";
            foreach (DataRow i in DBHelper.Instance.GetRecord(query).Rows)
            {
                list.Add(i["name"].ToString());
            }
            return list;
        }

        public string GetClassNameByID_DAL(int ID)
        {
            string query = "select NameLop from LopSH where ID_Lop = " + ID.ToString();
            DataRow dr = DBHelper.Instance.GetRecord(query).Rows[0];
            return dr["NameLop"].ToString();
        }

        public Student GetStudentByID_DAL(string ID)
        {
            string query = "select * from SV where MSSV = '" + ID + "'";
            return Get1Student(DBHelper.Instance.GetRecord(query).Rows[0]);
        }

        public void AddStudent_DAL(Student s)
        {
            string query = "insert into SV values ('";
            query += s.StudentID + "', N'" + s.Name + "', '" + s.Gender.ToString() + "', '"
                + s.DateOfBirth + "', " + s.ClassID.ToString() + ")";
            DBHelper.Instance.ExcuteDB(query);
        }

        public void EditStudent_DAL(Student s)
        {
            string query = "update SV set NameSV = N'" + s.Name + "', Gender = '" + s.Gender.ToString() + "', NS = '"
                + s.DateOfBirth + "', ID_Lop = " + s.ClassID.ToString() + " where MSSV = '" + s.StudentID + "'";
            DBHelper.Instance.ExcuteDB(query);
        }

        public void DeleteStudent_DAL(string StudentID)
        {
            string query = "delete from SV where MSSV = '" + StudentID + "'";
            DBHelper.Instance.ExcuteDB(query);
        }
    }
}

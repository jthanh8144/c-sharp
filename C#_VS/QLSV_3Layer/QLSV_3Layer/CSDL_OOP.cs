using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLSV_3Layer
{
    class CSDL_OOP
    {
        public List<Student> ListStudent;
        public List<Class> ListClass;
        public string[] listName = new string[CSDL.Instance.DTSV.Columns.Count];
        public delegate bool Compare(Student s1, Student s2);

        public static CSDL_OOP Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new CSDL_OOP();
                }
                return _Instance;
            }
            private set
            {

            }
        }

        private static CSDL_OOP _Instance;

        public Student get1Student(DataRow dr)
        {
            Student s = new Student();
            s.StudentID = dr["StudentID"].ToString();
            s.Name = dr["Name"].ToString();
            s.Gender = Convert.ToBoolean(dr["Gender"]);
            s.DateOfBirth = Convert.ToDateTime(dr["DateOfBirth"]);
            s.ClassID = Convert.ToInt32(dr["ClassID"]);
            return s;
        }

        public void getAllStudent()
        {
            ListStudent = new List<Student>();
            foreach (DataRow dr in CSDL.Instance.DTSV.Rows)
            {
                ListStudent.Add(get1Student(dr));
            }
        }

        public Class getClass(DataRow dr)
        {
            Class c = new Class();
            c.ClassID = Convert.ToInt32(dr["ClassID"]);
            c.ClassName = dr["ClassName"].ToString();
            return c;
        }

        public void getAllClass()
        {
            ListClass = new List<Class>();
            foreach (DataRow dr in CSDL.Instance.DTLSH.Rows)
            {
                ListClass.Add(getClass(dr));
            }
        }

        public CSDL_OOP()
        {
            getAllStudent();
            getAllClass();
            for (int i = 0; i < CSDL.Instance.DTSV.Columns.Count; i++)
            {
                listName[i] = CSDL.Instance.DTSV.Columns[i].ColumnName;
            }
        }

        public List<Student> getStudent(string name)
        {
            List<Student> temp = new List<Student>();
            foreach (Student i in ListStudent)
            {
                if (i.Name == name)
                {
                    temp.Add(i);
                }
            }
            return temp;
        }

        public List<Student> getStudent(int ClassID, string name)
        {
            List<Student> temp = new List<Student>();
            if (name == "")
            {
                foreach (Student i in ListStudent)
                {
                    if (i.ClassID == ClassID)
                    {
                        temp.Add(i);
                    }
                }
            }
            else
            {
                foreach (Student i in ListStudent)
                {
                    if ((i.ClassID == ClassID) && (i.Name == name))
                    {
                        temp.Add(i);
                    }
                }
            }
            return temp;
        }

        public int IndexOf(string StudentID)
        {
            for (int i = 0; i < ListStudent.Count; i++)
            {
                if (ListStudent[i].StudentID == StudentID)
                {
                    return i;
                }
            }
            return -1;
        }

        public void AddStudentToCSDL(Student s)
        {
            ListStudent.Add(s);
            object[] o = new object[CSDL.Instance.DTSV.Columns.Count];
            o[0] = ListStudent[ListStudent.Count - 1].StudentID;
            o[1] = ListStudent[ListStudent.Count - 1].Name;
            o[2] = ListStudent[ListStudent.Count - 1].Gender;
            o[3] = ListStudent[ListStudent.Count - 1].DateOfBirth;
            o[4] = ListStudent[ListStudent.Count - 1].ClassID;
            CSDL.Instance.DTSV.Rows.Add(o);
            getAllStudent();
        }

        public void EditStudentInCSDL(Student s, int index)
        {
            ListStudent[index] = s;
            object[] o = new object[CSDL.Instance.DTSV.Columns.Count];
            o[0] = s.StudentID;
            o[1] = s.Name;
            o[2] = s.Gender;
            o[3] = s.DateOfBirth;
            o[4] = s.ClassID;
            CSDL.Instance.DTSV.Rows[index].ItemArray = o;
            getAllStudent();
        }

        public void DeleteStudentInCSDL(int index)
        {
            ListStudent.Remove(ListStudent[index]);
            CSDL.Instance.DTSV.Rows.Remove(CSDL.Instance.DTSV.Rows[index]);
            getAllStudent();
        }

        public List<Student> sortListStudent(List<Student> list, int propertyIndex)
        {
            Compare cmp;
            switch (propertyIndex)
            {
                case 0:
                    cmp = Student.CompareSID;
                    break;
                case 1:
                    cmp = Student.CompareName;
                    break;
                case 2:
                    cmp = Student.CompareGender;
                    break;
                case 3:
                    cmp = Student.CompareDoB;
                    break;
                default:
                    cmp = Student.CompareCID;
                    break;
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (cmp(list[i], list[j]))
                    {
                        Student temp = list[i];
                        list[i] = list[j];
                        list[j] = temp;
                    }
                }
            }
            return list;
        }
    }
}

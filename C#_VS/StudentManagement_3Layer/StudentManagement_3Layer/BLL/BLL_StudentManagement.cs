using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement_3Layer.DAL;
using StudentManagement_3Layer.DTO;

namespace StudentManagement_3Layer.BLL
{
    class BLL_StudentManagerment
    {
        public delegate bool Compare(Student s1, Student s2);

        public static BLL_StudentManagerment Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL_StudentManagerment();
                }
                return _Instance;
            }
            private set
            {
            }
        }
        private static BLL_StudentManagerment _Instance;

        public List<Student> GetListStudent_BLL()
        {
            return DAL_StudentManagerment.Instance.GetListStudent_DAL();
        }

        public List<Student> GetListStudent_BLL(int ClassID, string Name)
        {
            return DAL_StudentManagerment.Instance.GetListStudent_DAL(ClassID, Name);
        }

        public List<Class> GetListClass_BLL()
        {
            return DAL_StudentManagerment.Instance.GetListClass_DAL();
        }

        public List<string> GetColumnName_BLL()
        {
            return DAL_StudentManagerment.Instance.GetColumnName_DAL();
        }

        public string GetClassNameByID_BLL(int ID)
        {
            return DAL_StudentManagerment.Instance.GetClassNameByID_DAL(ID);
        }

        public Student GetStudentByID_BLL(string ID)
        {
            return DAL_StudentManagerment.Instance.GetStudentByID_DAL(ID);
        }

        public ViewStudent View1Student(Student s)
        {
            ViewStudent temp = new ViewStudent();
            temp.StudentID = s.StudentID;
            temp.Name = s.Name;
            temp.Gender = s.Gender;
            temp.DateOfBirth = s.DateOfBirth;
            temp.ClassName = DAL_StudentManagerment.Instance.GetClassNameByID_DAL(s.ClassID);
            return temp;
        }

        public List<ViewStudent> ViewListStudent(int ClassID, string Name)
        {
            List<ViewStudent> list = new List<ViewStudent>();
            if (ClassID == 0 && Name == "")
            {
                foreach (Student i in GetListStudent_BLL())
                {
                    list.Add(View1Student(i));
                }
            }
            else
            {
                foreach (Student i in GetListStudent_BLL(ClassID, Name))
                {
                    list.Add(View1Student(i));
                }
            }
            return list;
        }

        public void AddStudent_BLL(Student s)
        {
            DAL_StudentManagerment.Instance.AddStudent_DAL(s);
        }

        public void EditStudent_BLL(Student s)
        {
            DAL_StudentManagerment.Instance.EditStudent_DAL(s);
        }

        public void DeleteStudent_BLL(string StudentID)
        {
            DAL_StudentManagerment.Instance.DeleteStudent_DAL(StudentID);
        }

        public List<Student> GetListStudentDgv(List<string> ListStudentID)
        {
            List<Student> data = new List<Student>();
            foreach (string i in ListStudentID)
            {
                data.Add(DAL_StudentManagerment.Instance.GetStudentByID_DAL(i));
            }
            return data;
        }

        public List<ViewStudent> ListStudentSort(List<Student> list, int propertyIndex)
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
                        Student s = list[i];
                        list[i] = list[j];
                        list[j] = s;
                    }
                }
            }
            List<ViewStudent> temp = new List<ViewStudent>();
            foreach (Student i in list)
            {
                temp.Add(View1Student(i));
            }
            return temp;
        }
    }
}

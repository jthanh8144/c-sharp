using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_CodeFirst
{
    class BLL
    {
        private Data db = new Data();

        public static BLL Instance
        {
            get
            {
                if (_Instance == null)
                {
                    _Instance = new BLL();
                }
                return _Instance;
            }
            private set
            {

            }
        }
        private static BLL _Instance;

        public List<SV> GetListSV(int ID_Lop, string Name)
        {
            List<SV> list = new List<SV>();
            if (ID_Lop == 0 && Name == "")
            {
                var l = from p in db.SVs select p;
                list = l.ToList();
            }
            else if (ID_Lop == 0 && Name != "")
            {
                var l = from p in db.SVs
                        where (p.NameSV.ToUpper().Contains(Name.ToUpper()))
                        select p;
                list = l.ToList();
            }
            else
            {
                if (Name == "")
                {
                    var l = from p in db.SVs
                            where p.ID_Lop == ID_Lop
                            select p;
                    list = l.ToList();
                }
                else
                {
                    var l = from p in db.SVs
                            where (p.ID_Lop == ID_Lop && p.NameSV.ToUpper().Contains(Name.ToUpper()))
                            select p;
                    list = l.ToList();
                }
            }
            return list;
        }

        public SV GetSVByMSSV(string MSSV)
        {
            var sv = db.SVs.Where(p => p.MSSV == MSSV).FirstOrDefault();
            return sv;
        }

        public void AddStudent(SV s)
        {
            db.SVs.Add(s);
            db.SaveChanges();
        }

        public void EditStudent(SV s)
        {
            var sv = db.SVs.Where(p => p.MSSV == s.MSSV).FirstOrDefault();
            sv.NameSV = s.NameSV;
            sv.Gender = s.Gender;
            sv.NS = s.NS;
            sv.ID_Lop = s.ID_Lop;
            db.SaveChanges();
        }

        public void DeleteStudent(string MSSV)
        {
            db.SVs.Remove(GetSVByMSSV(MSSV));
            db.SaveChanges();
        }
    }
}

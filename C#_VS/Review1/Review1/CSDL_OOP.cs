using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review1
{
    class CSDL_OOP
    {
        public List<SinhVien> ArrayList;

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

        private CSDL_OOP()
        {
            getAllSV();
        }

        public SinhVien get1SV(DataRow dr)
        {
            SinhVien s;
            if (dr["LoaiHinh"].ToString() == "Đại học")
            {
                s = new SVDH();
                ((SVDH)s).ChuyenNganh = dr["ChuyenNganh"].ToString();
            }
            else if (dr["LoaiHinh"].ToString() == "Bằng hai")
            {
                s = new SVBH();
                ((SVBH)s).DVCT = dr["DVCT"].ToString();
                ((SVDH)s).ChuyenNganh = "1";
            }
            else
            {
                s = new SinhVien();
            }
            s.MSV = dr["MSV"].ToString();
            s.HoTen = dr["HoTen"].ToString();
            s.NgaySinh = Convert.ToDateTime(dr["NgaySinh"]);
            s.DiaChi = dr["DiaChi"].ToString();
            s.SDT = dr["SDT"].ToString();
            s.NienKhoa = dr["NienKhoa"].ToString();
            return s;
        }
        public void getAllSV()
        {
            ArrayList = new List<SinhVien>();
            foreach (DataRow i in CSDL.Instance.DB.Rows)
            {
                ArrayList.Add(get1SV(i));
            }
        }
    }
}

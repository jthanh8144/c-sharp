using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review1
{
    class CSDL
    {
        public DataTable DB { get; set; }

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

        private static CSDL _Instance;

        private CSDL()
        {
            DB = new DataTable();
            DB.Columns.AddRange(new DataColumn[]
            {
                new DataColumn( "MSV", typeof(string)),
                new DataColumn( "HoTen", typeof(string)),
                new DataColumn( "NgaySinh", typeof(DateTime)),
                new DataColumn( "DiaChi", typeof(string)),
                new DataColumn( "SDT", typeof(string)),
                new DataColumn( "Nienkhoa", typeof(string)),
                new DataColumn( "LoaiHinh", typeof(string)),
                new DataColumn( "ChuyenNganh", typeof(string)),
                new DataColumn( "DVCT", typeof(string))
            });

            DataRow dr = DB.NewRow();
            dr["MSV"] = "101";
            dr["HoTen"] = "NVA";
            dr["NgaySinh"] = DateTime.Now;
            dr["DiaChi"] = "QN";
            dr["SDT"] = "0123";
            dr["NienKhoa"] = "19-23";
            dr["LoaiHinh"] = "Đại học";
            dr["ChuyenNganh"] = "CNPM";
            dr["DVCT"] = "";
            DB.Rows.Add(dr);

            DataRow dr1 = DB.NewRow();
            dr1["MSV"] = "102";
            dr1["HoTen"] = "NVB";
            dr1["NgaySinh"] = DateTime.Now;
            dr1["DiaChi"] = "Hue";
            dr1["SDT"] = "0124";
            dr1["NienKhoa"] = "18-21";
            dr1["LoaiHinh"] = "Cao đẳng";
            dr1["ChuyenNganh"] = "";
            dr1["DVCT"] = "";
            DB.Rows.Add(dr1);

            DataRow dr2 = DB.NewRow();
            dr2["MSV"] = "103";
            dr2["HoTen"] = "NVC";
            dr2["NgaySinh"] = DateTime.Now;
            dr2["DiaChi"] = "DN";
            dr2["SDT"] = "0125";
            dr2["NienKhoa"] = "19-24";
            dr2["LoaiHinh"] = "Bằng hai";
            dr2["ChuyenNganh"] = "1";
            dr2["DVCT"] = "DUT";
            DB.Rows.Add(dr2);
        }
    }
}

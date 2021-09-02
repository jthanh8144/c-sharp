using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review1
{
    class SinhVien
    {
        public string MSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string NienKhoa { get; set; }

        public virtual string LoaiHinh()
        {
            return "Cao đẳng";
        }
    }
}

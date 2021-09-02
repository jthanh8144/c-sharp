using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review1
{
    class SVDH : SinhVien
    {
        public string ChuyenNganh { get; set; }

        public override string LoaiHinh()
        {
            return "Đại học";
        }
    }
}

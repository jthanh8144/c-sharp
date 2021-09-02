using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Review1
{
    class SVBH : SVDH
    {
        public string DVCT { get; set; }

        public override string LoaiHinh()
        {
            return "Bằng hai";
        }
    }
}

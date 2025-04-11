using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_CodeFirst
{
    [Table("SinhVien")]
    public class SV
    {
        [Key]
        public string MSSV { get; set; }
        public string NameSV { get; set; }
        public bool Gender { get; set; }
        public DateTime NS { get; set; }
        public int ID_Lop { get; set; }

        [ForeignKey("ID_Lop")]
        public virtual LopSH LopSH { get; set; }
    }
}

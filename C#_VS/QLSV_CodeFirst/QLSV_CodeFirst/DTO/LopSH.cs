using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSV_CodeFirst
{
    [Table("LopSH")]
    public class LopSH
    {
        public LopSH()
        {
            SVs = new HashSet<SV>();
        }
        [Key]
        public int ID_Lop { get; set; }
        public string NameLop { get; set; }
        public virtual ICollection<SV> SVs { get; set; }
    }
}

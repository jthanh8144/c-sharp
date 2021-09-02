using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityCodeFirst
{
    public class CreateDB :
        CreateDatabaseIfNotExists<Model1>
    {
        protected override void Seed(Model1 context)
        {
            context.LSHes.AddRange(new LSH[]
            {
                new LSH {ID_Lop = 1, NameLop = "LSH1"},
                new LSH {ID_Lop = 2, NameLop = "LSH2"}
            });
            context.SVs.AddRange(new SV[]
            {
                new SV { MSSV = "101", NameSV = "NVA", Gender = true, NS = DateTime.Now, ID_Lop = 1},
                new SV { MSSV = "102", NameSV = "NVB", Gender = false, NS = DateTime.Now, ID_Lop = 2},
                new SV { MSSV = "103", NameSV = "NAC", Gender = true, NS = DateTime.Now, ID_Lop = 1}
            });
        }
    }
}

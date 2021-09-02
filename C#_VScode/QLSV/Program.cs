using System;
using System.Collections.Generic;

namespace QLSV
{
    class Program
    {
        static void Main(string[] args)
        {
            /*QLSV db = new QLSV();
            SV s1 = new SV(1, "A", 1.1);
            SV s2 = new SV(2, "B", 2.2);
            SV s3 = new SV(3, "C", 2.2);
            SV s4 = new SV(4, "D", 2.2);
            db.Add(s2);
            db.Add(s3);
            db.Add(s1);
            db.Add(s2);
            db.Add(s1);
            db.Insert(s2,2);
            db.RemoveAt(0);
            Console.WriteLine(db.ToString());
            //Console.WriteLine(db.IndexOf(s2));
            db.Remove(s2);
            //db.Edit(2);
            db.Sort();
            Console.WriteLine(db.ToString());*/
            /*SV s1 = new SV(1, "A", 1.1);
            SV s2 = new SV(2, "B", 2.2);
            SV s3 = new SV(3, "C", 2.2);
            SV s4 = new SV(4, "D", 2.2);
            QLSV.Instance.Add(s1);
            Console.WriteLine(QLSV.Instance.ToString());*/
            List<SV> l = new List<SV>();
            SV s1 = new SV(1, "A", 1.1);
            SV s2 = new SV(2, "B", 2.2);
            SV s3 = new SV(3, "C", 2.2);
            SV s4 = new SV(4, "D", 2.2);
            l.Add(s1);
            l.AddRange(new SV[] {s3, s2});
            int index = l.IndexOf(s4);
            Console.WriteLine(index);
            l.Remove(s3);
            foreach (SV i in l)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }
}

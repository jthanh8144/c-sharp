using System;

namespace QLSV
{
    class SV
    {
        public int MSV {get; set;}
        public string NameSV {get; set;}
        public double DTB {get; set;}
        public SV(int m, string n, double d)
        {
            MSV = m;
            NameSV = n;
            DTB = d;
        }
        public void Show()
        {
            Console.WriteLine("MSV: " + MSV + ", NameSV: " + NameSV + ", DTB: " + DTB);
        }
        public override string ToString()
        {
            return "MSV: " + MSV + ", NameSV: " + NameSV + ", DTB: " + DTB;
        }
    }
}
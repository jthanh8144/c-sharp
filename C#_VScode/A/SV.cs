using System;

namespace C_
{
    class SV
    {
        public int MSSV {get; set;}
        public string NameSV {get; set;}
        public double DTB {get; set;} 
        public SV(int m, string n, double d)
        {
            MSSV = m;
            NameSV = n;
            DTB = d;
        }
        public virtual void Show()
        {
            Console.WriteLine("MSV: " + MSSV + ", Name: " + NameSV + ", DTB: " + DTB);
        }
        public override string ToString()
        {
            return "MSV: " + MSSV + ", Name: " + NameSV + ", DTB: " + DTB;
        }
        public void Thi()
        {
            Console.WriteLine("Thi tu luan");
        }
    }
}
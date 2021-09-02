using System;

namespace C_
{
    class SVCNTT : SV
    {
        public bool skill {get; set;}
        public SVCNTT(int m, string n, double d, bool s) 
            : base(m, n, d)
        {
            skill = s;
        }
        public override void Show()
        {
            Console.WriteLine("MSV: " + MSSV + ", Name: " + NameSV + ", DTB: " + DTB + ", Skill: " + skill);
        }
        public new void Thi()
        {
            Console.WriteLine("Lap trinh");
        }
    }
}
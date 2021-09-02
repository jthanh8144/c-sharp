using System;

namespace DelegateSort
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
        public static bool CompareMSV(object s1, object s2)
        {
            if (((SV)s1).MSV >= ((SV)s2).MSV) return true;
            else return false;
        }
        public static bool CompareName(object s1, object s2)
        {
            if (string.Compare(((SV)s1).NameSV, ((SV)s2).NameSV) >= 0) return true;
            else return false;
        }
    }
}
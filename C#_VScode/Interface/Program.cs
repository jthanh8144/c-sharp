using System;

namespace Interface
{
    class Program
    {
        static void Main(string[] args)
        {
            /*MyClass c = new MyClass();
            c.A();
            c.B();
            IFace1 f = (IFace1)c;
            f.A();
            f.B();*/
            MyClass f = new MyClass();
            IFace1 f1 = (IFace1) f;
            IFace2 f2 = (IFace2) f;
            f1.A();
            f2.A();
            f1.B();
            f2.C();
        }
    }
}

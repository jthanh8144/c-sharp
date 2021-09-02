using System;

namespace Interface
{
    public class MyClass : IFace1, IFace2
    {
        void IFace1.A()
        {
            Console.WriteLine("A1");
        }
        void IFace2.A()
        {
            Console.WriteLine("A2");
        }

        void IFace1.B()
        {
            Console.WriteLine("B1");
        }
        void IFace2.C()
        {
            Console.WriteLine("C2");
        }
        public void F()
        {
            Console.WriteLine("F0");
        }
    }
}
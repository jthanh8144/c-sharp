using System;

namespace Delegate
{
    class Program
    {
        public delegate int MyDel(int x, int y); // Khi tham chiếu tới nhiều hàm thì nên để void
        public static int Add(int x, int y)
        {
            Console.WriteLine("x + y = " + (x + y));
            return x + y;
        }
        public static int Sub(int x, int y)
        {
            Console.WriteLine("x - y = " + (x - y));
            return x - y;
        }
        public static int TinhToan(int x, int y, MyDel d)
        {
            return d(x, y);
        }
        static void Main(string[] args)
        {
            /*MyDel d = new MyDel(Add);
            int z = d(3, 4);
            Console.WriteLine(z);
            int t = d.Invoke(3, 4);
            Console.WriteLine(t);*/
            /*MyDel d1 = new MyDel(Add);
            int z1 = TinhToan(3, 4, d1);
            Console.WriteLine(z1);
            int z2 = TinhToan(3, 4, Sub);
            Console.WriteLine(z2);*/
            MyDel d = new MyDel(Add);
            d += new MyDel(Sub);
            d += new MyDel(Sub);
            int t = d(4, 3);
            Console.WriteLine(t);
        }
    }
}

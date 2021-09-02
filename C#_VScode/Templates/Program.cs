using System;

namespace Templates
{
    class Program
    {
        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
        static void Main(string[] args)
        {
            /*int a = 10, b = 90;
            Console.WriteLine("Before: " + a + " " + b);
            Swap<int>(ref a, ref b);
            Console.WriteLine("After: "+ a + " " + b);
            string c = "Lo cc", d = "Lo cl";
            Console.WriteLine("Before: " + c + " " + d);
            Swap<string>(ref c, ref d);
            Console.WriteLine("After: "+ c + " " + d);*/
            SV s1 = new SV(1, "A", 1.1);
            SV s2 = new SV(2, "B", 2.2);
            SV s3 = new SV(3, "C", 2.2);
            SV s4 = new SV(4, "D", 2.2);
            ListT<SV>.Instance.Add(s1);
            ListT<SV>.Instance.Add(s2);
            ListT<SV>.Instance.Add(s3);
            ListT<SV>.Instance.Add(s4);
            Console.WriteLine(ListT<SV>.Instance.IndexOf(s3));
        }
    }
}

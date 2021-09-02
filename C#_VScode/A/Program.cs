using System;
using System.Collections;

namespace C_
{
    
    class Program
    {
        // static void A(out int a)
        // {
        //     a = 5;
        // }
        // static void HV(ref int x, ref int y)
        // {
        //     int temp = x;
        //     x = y;
        //     y = temp;
        // }
        // static int sum(params int[] a)
        // {
        //     int temp = 0;
        //     for (int i = 0; i < a.GetLength(0); i++)
        //         temp += a[i];
        //     return temp;
        // }
        static void Main(string[] args)
        {
            Console.WriteLine('a'|'b'|'c');

            // Console.WriteLine("Hello World!");
            // int x = 1, y, z;
            // Console.Write("x = ");
            // x = int.Parse(Console.ReadLine());
            // Console.WriteLine(x);
            // Console.Write("y = ");
            // int.TryParse(Console.ReadLine(), out y);
            // Console.WriteLine(y);
            // Console.Write("z = ");
            // z = Convert.ToInt32(Console.ReadLine());
            // Console.WriteLine(z);

            /*int x, y, z;
            Console.Write("x = ");
            x = Convert.ToInt32(Console.ReadLine());
            Console.Write("y = ");
            y = Convert.ToInt32(Console.ReadLine());
            HV(ref x, ref y);
            Console.WriteLine("x = " + x + " y = " + y);
            A(out z);
            Console.WriteLine("z = " + z);*/
            /*int n;
            Console.Write("Nhap n = ");
            n = Convert.ToInt32(Console.ReadLine());
            string[] A = new string[n];
            for (int i = 0; i < n; i++){
                Console.Write("A[" + i + "] = ");
                A[i] = Console.ReadLine();
            }
            Console.WriteLine("Xuat: ");
            for (int i = 0; i < n; i++){
                Console.WriteLine("A[" + i + "] = " + A[i]);
            }*/
            /*string[,] arr = new string[2, 3]
            {
                {"a1", "a2", "a3"},
                {"b1", "b2", "b3"}
            };
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }
            foreach(string i in arr)
            {
                Console.Write(i + " ");
            }*/
            /*string[][] softwares = new string[3][];
            softwares[0] = new string[]
            {
                "Bit", "Kar", "NAV"
            };
            softwares[1] = new string[]
            {
                "A", "B", "C", "D"
            };
            softwares[2] = new string[]
            {
                "1", "2"
            };
            for (int i = 0; i < softwares.GetLength(0); i++)
            {
                for (int j = 0; j < softwares[i].GetLength(0); j++)
                    Console.Write(softwares[i][j] + " ");
                Console.WriteLine();
            }
            foreach(string[] i in softwares)
            {
                foreach(string j in i)
                {
                    Console.Write(j + " ");
                }
                Console.WriteLine();
            }*/
            /*int[] a = {1, 2, 3, 4};
            int x = sum(a);
            int y = sum(1, 2, 3, 4);
            Console.WriteLine(x);
            Console.WriteLine(y);*/
            /*SV s1 = new SV
            {
                MSSV = 1, NameSV = "NVA", DTB = 1.1
            };
            s1.Show();
            Console.WriteLine(s1.ToString());*/
            /*SV s = new SV(2, "NVB", 2.2);
            SVCNTT s1 = new SVCNTT(1, "NVA", 1.1, true);
            s1.Show();
            s.Thi();
            s1.Thi();
            ((SV)s1).Thi();*/
        }
    }
}
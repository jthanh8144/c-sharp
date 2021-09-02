using System;

namespace DelegateSort
{
    class Program
    {
        public delegate bool Compare(object s1, object s2);
        public static void Sorts(SV[] arr, Compare cmp)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (cmp(arr[i], arr[j]))
                    {
                        SV temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            SV[] arr = new SV[]
            {
                new SV(2, "A", 1.1),
                new SV(4, "B", 2.2),
                new SV(1, "C", 2.2),
                new SV(3, "D", 2.2)
            };
            Sorts(arr, SV.CompareMSV);
            foreach (SV i in arr)
            {
                Console.WriteLine(i.ToString());
            }
        }
    }
}

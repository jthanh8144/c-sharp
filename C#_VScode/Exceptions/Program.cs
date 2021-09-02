using System;

namespace Exceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int a = 5;
                int b = 0;
                Console.WriteLine(a/b);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chung: " + ex.Message);
            }
            catch (DivideByZeroException dex)
            {
                Console.WriteLine("Mau bang 0: " + dex.Message);
            }
            // finally
            // {
            //     Console.WriteLine();
            //     Console.WriteLine("Alo");
            // }
            
        }
    }
}

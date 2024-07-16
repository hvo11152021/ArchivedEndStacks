using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_BasicAlgorithms
{
    internal class Program
    {
        //static void Main(string[] args)
        //{
        //    Console.WriteLine(test(1, 2));
        //    Console.WriteLine(test(3, 2));
        //    Console.WriteLine(test(2, 2));
        //    Console.ReadLine();
        //}

        //public static int test(int x, int y)
        //{
        //    return x == y ? (x + y) * 3 : x + y;
        //}

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(test(53));
        //    Console.WriteLine(test(30));
        //    Console.WriteLine(test(51));
        //    Console.ReadLine();
        //}

        //public static int test(int n)
        //{
        //    const int x = 51;
        //    if (n > x)
        //    {
        //        return (n - x) * 3;
        //    }
        //    return x - n;
        //}

        static void Main(string[] args)
        {
            Console.WriteLine(test(30, 0));
            Console.WriteLine(test(25, 5));
            Console.WriteLine(test(20, 30));
            Console.WriteLine(test(20, 25));
            Console.ReadLine();
        }

        public static bool test(int x, int y)
        {
            return x == 30 || y == 30 || (x + y == 30);
        }
    }
}

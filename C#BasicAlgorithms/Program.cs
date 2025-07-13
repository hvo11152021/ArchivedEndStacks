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

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(test(30, 0));
        //    Console.WriteLine(test(25, 5));
        //    Console.WriteLine(test(20, 30));
        //    Console.WriteLine(test(20, 25));
        //    Console.ReadLine();
        //}

        //public static bool test(int x, int y)
        //{
        //    return x == 30 || y == 30 || (x + y == 30);
        //}

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(test(103));
        //    Console.WriteLine(test(90));
        //    Console.WriteLine(test(89));
        //    Console.ReadLine();
        //}

        //public static bool test(int x)
        //{
        //    if (Math.Abs(x - 100) <= 10 || Math.Abs(x - 200) <= 10)
        //        return true;
        //    return false;
        //}

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(test("if else"));
        //    Console.WriteLine(test("else"));
        //    Console.ReadLine();
        //}

        //public static string test(string s)
        //{
        //    if (s.Length > 2 && s.Substring(0, 2).Equals("if"))
        //    {
        //        return s;
        //    }
        //    return "if " + s;
        //}

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(test("Python", 1));
        //    Console.WriteLine(test("Python", 0));
        //    Console.WriteLine(test("Python", 4));
        //    Console.ReadLine();
        //}

        //public static string test(string str, int n)
        //{
        //    return str.Remove(n, 1);
        //}

        //static void Main(string[] args)
        //{
        //    Console.WriteLine(test("abcd"));
        //    Console.WriteLine(test("a"));
        //    Console.WriteLine(test("xy"));
        //    Console.ReadLine();
        //}

        //public static string test(string str)
        //{
        //    return str.Length > 1
        //        ? str.Substring(str.Length - 1) + str.Substring(1, str.Length - 2) + str.Substring(0, 1)
        //        : str;
        //}

        static void Main(string[] args)
        {
            Console.WriteLine(test("C Sharp"));
            Console.WriteLine(test("JS"));
            Console.WriteLine(test("a"));
            Console.ReadLine();
        }

        public static string test(string str)
        {
            return str.Length < 2
                ? str
                : str.Substring(0, 2) + str.Substring(0, 2) + str.Substring(0, 2) + str.Substring(0, 2);
        }
    }
}

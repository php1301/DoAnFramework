using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = Console.ReadLine();
            SqlStrategy s = new SqlStrategy("abc.com","69","69","3000");
            Console.WriteLine(a);
        }
    }
}

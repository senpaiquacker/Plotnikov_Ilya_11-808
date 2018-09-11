using System;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            int x = int.Parse(Console.ReadLine());
            int y = int.Parse(Console.ReadLine());
            x += y;
            y = x - y;
            x = x - y;
        }
    }
}

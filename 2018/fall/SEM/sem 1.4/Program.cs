using System;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = int.Parse(Console.ReadLine());
            int X = int.Parse(Console.ReadLine());
            int Y = int.Parse(Console.ReadLine());
            Console.WriteLine((N-1)/X+(N-1)/Y-(N-1)/(X*Y));
        }
    }
}

using System;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {
            //input N,X,Y
            int N = int.Parse(Console.ReadLine());
            int X = int.Parse(Console.ReadLine());
            int Y = int.Parse(Console.ReadLine());
            //simple calculation of union of sets of numbers can be devided by X or Y
            Console.WriteLine((N-1)/X+(N-1)/Y-(N-1)/(X*Y));
        }
    }
}

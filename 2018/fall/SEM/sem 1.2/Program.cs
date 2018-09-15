using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //main number
            int mn = int.Parse(Console.ReadLine());
            //new number
            int nn = 0;
            //swap operation
            nn += (mn % 10) * 100;
            mn = (mn - (mn % 10)) / 10;
            nn += (mn % 10) * 10;
            mn = (mn - (mn % 10)) / 10;
            nn += mn % 10;
            //then value goes back to mn
            mn=nn;
            Console.WriteLine(mn);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}

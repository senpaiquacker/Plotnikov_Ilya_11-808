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
            nn += (mn % 10) * 100;
            mn = (mn - (mn % 10)) / 10;
            nn += (mn % 10) * 10;
            mn = (mn - (mn % 10)) / 10;
            nn += mn % 10;
            Console.WriteLine(nn);
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}

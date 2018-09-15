using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
    class Program
    {
        static void Main(string[] args)
        {
            //input our point
            int x1 = int.Parse(Console.ReadLine());
            int y1 = int.Parse(Console.ReadLine());
            //input our line
            int x2 = int.Parse(Console.ReadLine());
            int x3 = int.Parse(Console.ReadLine());
            int y2 = int.Parse(Console.ReadLine());
            int y3 = int.Parse(Console.ReadLine());
            //building triangle
            double a = (double) Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
            double b = (double) Math.Sqrt((x3 - x1) * (x3 - x1) + (y3 - y1) * (y3 - y1));
            double c = (double) Math.Sqrt((x3 - x2) * (x3 - x2) + (y3 - y2) * (y3 - y2));
            //calculating Square of triangle
            double p = (a + b + c) / 2;
            double S = (double)Math.Sqrt(p * (p - a) * (p - b) * (p - c));
            //finding height
            double h = S * 2 / c;
            Console.WriteLine(h);

        }
    }
}

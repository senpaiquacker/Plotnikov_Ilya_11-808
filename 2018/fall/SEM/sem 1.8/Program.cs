using System;
using System.Globalization;
namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            //inputting position of dots included in line
            double k = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            double m = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            //inputting dot position
            double px = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            double py = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
            //finding our m in y = -(1/k)*x+m
            double m2 = py + (1 / k) * px;
            //finding x pos:
            double v = (k - (1 / k));
            double x2 = (m2 - m) / v;
            //finding y pos:
            double y2 = k * x2 + m;
            //output
            Console.Write(x2);
            Console.Write(" ");
            Console.Write(y2);
            Console.ReadKey();
        }
    }
}

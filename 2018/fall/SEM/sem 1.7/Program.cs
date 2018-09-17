using System;
using System.Globalization;
namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //entering k and m
            double k = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);
            double m = double.Parse(Console.ReadLine(),CultureInfo.InvariantCulture);
            //adding variables for position of both vectors and instantly finding it
            //because we don't need an exact vector, we can set potential x1 = 0, potential x2 = 1, and y1 = k * x1 + m, y2 = k * x2 + m then
            //so coordinates of our vector will be (1 - 0; k * 1 + m - (k * 0 + m)) => (1; k)
            // but this is only for parallel vector. For perpendicular our y1 = (-1) * (1/k) * 0 + m, y2 = (-1) * (1/k) * 1 + m
            //and these coordinates will be (1; -1/k);
            double vec1x = 1;
            double vec1y = k;
            double vec2x = 1;
            double vec2y = (-1) * (1 / k);
            //output
            Console.Write(vec1x);
            Console.Write(" ");
            Console.WriteLine(vec1y);
            Console.Write(vec2x);
            Console.Write(" ");
            Console.WriteLine(vec2y);
            Console.ReadKey();
        }
    }
}

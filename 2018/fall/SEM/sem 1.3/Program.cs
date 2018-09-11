using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            //our main variable
            int hour = int.Parse(Console.ReadLine());
            //our main counter k, to count, how many time left since last '6' or '12'
            int k = 0;
            //this is cycle to count it
            while(hour>6)
            {
                k++;
                hour -= 6;
            }
            //now we transform our counter to a bool-like variable, which shows, are we over 6 o'clock or not
            k = k % 2;
            //counting degrees clockwise
            int answ = hour * 30;
            //checking our k on truth
            while (k>0)
            {
                k--;
                answ = 180 - answ;
            }
            Console.WriteLine("The answer is:");
            Console.WriteLine(answ);
            Console.ReadKey();
        }
    }
}

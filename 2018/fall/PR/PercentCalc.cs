using System;
using System.Globalization;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            string userIn = Console.ReadLine();
            double answ = Calculate(userIn);
            Console.WriteLine(answ);
            Console.ReadKey();
        }

        static double Calculate(string input)
        {
            //transforming string into variables (finding all substring till first " ", and then parsing)
            double budget = double.Parse(input.Substring(0, input.IndexOf(" ")), CultureInfo.InvariantCulture);
            input = input.Remove(0,input.IndexOf(" ")+1);
            double percent = double.Parse(input.Substring(0, input.IndexOf(" ")), CultureInfo.InvariantCulture);
            //transforming percent into numeric variant, which is percent/100
            //then percent/12 (because it was year percent, and we need month)
            //then +1 (final stage of transformation, which means we will take our previous money and add percent)
            percent /= (12.0 * 100.0);
            percent += 1;
            input = input.Remove(0,input.IndexOf(" ")+1);
            int months = int.Parse(input);
            //because we do nothing with these money, our formula will be like S(final sum of money)=S0(at the beginning)*percent...*percent
            //so we're going to return this, instead of cycle:
            return budget * Math.Pow(percent, (double)months);
        }
    }
}

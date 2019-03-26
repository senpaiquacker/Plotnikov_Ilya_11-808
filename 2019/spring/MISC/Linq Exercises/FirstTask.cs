using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            var stringArray = new string[n];
            for(int i = 0; i < n; i++)
            {
                stringArray[i] = Console.ReadLine();
            }
            int k = 1;
            var answer = stringArray
                .Where(z => z != string.Empty)
                .Select(z => z + k++);
            foreach(var i in answer)
            {
                Console.WriteLine(i);
            }
            Console.ReadKey();
        }
    }
}

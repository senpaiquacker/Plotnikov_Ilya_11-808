using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Hello";
            str = str.Substring(str.Length / 2, str.Length);
            Console.WriteLine(str);
            Console.ReadKey();
        }
    }
}

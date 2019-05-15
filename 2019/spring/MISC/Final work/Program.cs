using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace FirstTask
{
    class Info
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
       
    }
    class Program
    {
        public static void CountSymbols(Info code)
        {
            if (code.id % 2 == 0)
            {
                var codeFilter = code
                                 .body
                                 .Where(new Func<char, bool>(symbol =>
                                 (symbol >= 'a' && symbol <= 'z') ||
                                 (symbol >= 'A' && symbol <= 'Z')));
                var amount = 0;
                foreach (var symbol in codeFilter)
                    amount++;
                Console.WriteLine(amount);
            }
            else Console.WriteLine("null");
        }

        static void Main(string[] args)
        {
            var code = JsonConvert.DeserializeObject<List<Info>>
                (File.ReadAllText(@"C:\Users\Варфоломеев\Desktop\json.txt"));
            Parallel.ForEach(code, CountSymbols);
        }
    }
}

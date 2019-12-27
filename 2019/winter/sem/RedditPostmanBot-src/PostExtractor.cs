using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
namespace EchoBot
{
    public class PostExtractor
    {
        public static void Begin()
        {
            var address = new Uri(@"http://localhost:3978/api/notify");
            var client = new HttpClient();
            client.GetAsync(address);
        }
    }
}

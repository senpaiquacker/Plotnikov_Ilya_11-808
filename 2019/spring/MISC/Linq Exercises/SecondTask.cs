using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    static class IEnumerableExtensions
    {
        public static IEnumerable<TItem> MakeStrangeThing<TItem>
            (this IEnumerable<TItem> ienumerable, Func<int, IEnumerable<TItem>> function)
        {
            int i = 0;
            var result = new List<TItem>();
            foreach(var item in ienumerable)
            {
                result.Add(item);
                if(i % 2 !=0)
                {
                    result.AddRange(function(i));
                }
            }
            return result as IEnumerable<TItem>;
        }
    }
}

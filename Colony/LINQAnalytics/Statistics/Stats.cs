using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQAnalytics.Statistics
{
    public static class Stats
    {
        public static double Percentile<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int total = 0;
            int count = 0;

            foreach (T item in source)
            {
                ++count;
                if (predicate(item))
                {
                    total += 1;
                }
            }

            return (100.0 * total) / count;
        }
    }
}

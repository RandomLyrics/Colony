using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicLambda.Example
{
    public class ExampleRun
    {
        public void Run()
        {
            var dic = new Dictionary<int, Task>();
            var que = new Queue<Task>();
            var b1 = GC.GetTotalMemory(false);
            Console.WriteLine(b1);
            var ss = new Dictionary<string, long>();
            for (int i = 0; i < 10000; i++)
            {
                ss[i.ToString()] = i % 100;
            }
            Console.WriteLine("dictionary");
            Console.WriteLine(GC.GetTotalMemory(false) - b1);
            Console.WriteLine("  ");
            b1 = GC.GetTotalMemory(false);
            Console.WriteLine(b1);
            var dd = new List<string>();
            for (int i = 0; i < 10000; i++)
            {
                dd.Add((i * i).ToString());
            }
            Console.WriteLine("list");
            Console.WriteLine(GC.GetTotalMemory(false) - b1);
            Console.WriteLine("asdsad");
        }
    }
}

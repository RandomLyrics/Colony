using Hermes;
using OrionOld.Engine;
using OrionOld.Example;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrionOld
{
    class Program
    {
        public static  async void Run(int i)
        {
            Console.WriteLine("started");
            await DoSomething(i).ConfigureAwait(false);
            Console.WriteLine("finished " + i);
        }

        private static Task DoSomething(int i)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("done something " + i);
            });
           
            //return Run();
        }

        static void Main(string[] args)
        {
            //Run(1);
            //Run(2);
            //Run(3);
            //Console.ReadKey();

            var ss = new List<string>();
            ss.Add("sadasd");
            foreach (var item in ss)
            {
                ss.Add("asdasd");
            }
        }
    }
}

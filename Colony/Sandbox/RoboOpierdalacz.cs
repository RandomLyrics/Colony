using Sandbox.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox
{
    public class RoboOpierdalacz
    {
        public void Dupa([FromNice]Company comp)
        {

        }
        public void Run()
        {
            Random r = new Random();
            Console.WriteLine("Processing...");
            var count = r.Next(10000, 50000);
            for (int i = 0; i < count; i++)
            {
                if (r.Next(0, 100) <= 90)
                    Console.WriteLine("{0}/{1}", i, count);
                else
                    Console.WriteLine("{0}/{1} - error, check log.txt", i, count);

                Thread.Sleep(r.Next(500, 2000));
            }
        }
    }
}

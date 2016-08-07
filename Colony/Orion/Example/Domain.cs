using Orion.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Example
{
    public class Domain: Shared
    {
        public Data Data { get; set; }
        public Logic Logic { get; set; }
        
        public Domain()
        {
            this.Cache = new Cache();
            for (int i = 0; i < 10; i++)
            {
                Stopwatch timer = new Stopwatch();
                timer.Start();

                Hierarchy.Build(this);
                //var ss = this.Build<Domain>();

                timer.Stop();
                Console.WriteLine("Elpased: " + timer.ElapsedTicks);
            }
        }
    }
}

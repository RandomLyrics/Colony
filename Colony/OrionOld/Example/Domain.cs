using OrionOld.Engine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionOld.Example
{
    public class Domain: Shared
    {
        public Data Data { get; set; }
        public Logic Logic { get; set; }

        public int dummy { get; set; }
        public int dupa { get; set; }
        public int dupsko { get; set; }
        public override void Before()
        {
            
            //this.FillProperties(this);
        }
        public Domain()
        {
            //this.Cache = new Cache();
            //for (int i = 0; i < 10; i++)
            //{
            //    Stopwatch timer = new Stopwatch();
            //    timer.Start();

            //    Hierarchy.Build(this);
            //    //var ss = this.Build<Domain>();

            //    timer.Stop();
            //    Console.WriteLine("Elpased: " + timer.ElapsedMilliseconds);
            //}
            // Data = new Data() { Cache = this.Cache };
            // this.Cache = new Cache();
            //this.ToCache(Cache);
            //this.InjectProperties
            //this.InjectProperties(this, Cache);
            //Cache.Insert(ref Data);
            //Cache.Insert(ref Logic);
            //FromCache.Inject(this);
        }
    }
}

using Orion.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Example
{
    public class Domain: Shared
    {
        public Data Data { get; set; }
        public Logic Logic { get; set; }

        public int dummy { get; set; }

        public override void Before()
        {
            this.Cache = new Cache();
            this.InjectProperties(this);
        }
        public Domain() : base()
        {
            Data = new Data() { Cache = this.Cache };
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

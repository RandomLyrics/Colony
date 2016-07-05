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

        public Domain()
        {
            this.Cache = new Cache();
            this.InjectProperties(this, Cache);
            //Cache.Insert(ref Data);
            //Cache.Insert(ref Logic);
            //FromCache.Inject(this);
        }
    }
}

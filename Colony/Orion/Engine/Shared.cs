using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Engine
{
    public class Shared: IShareable
    {
        //PROPS
        private Cache _cache;
        public Cache Cache
        {
            get { return _cache; }
            set { _cache = value; }
        }

        //CTORS
        public Shared()
        {
            this._cache = new Cache();
            InjectProperties();
        }
        public Shared(Cache cache)
        {
            this._cache = cache;
        }

        //METHODS
        private void InjectProperties()
        {
            Injector.Inject(this, Cache);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Engine
{
    public class Cache
    {
        private IDictionary<Type, IShareable> _cache;

        public IShareable this[Type x]
        {
            get { return _cache[x]; }
            set { _cache[x] = value; }
        }
        
        public Cache()
        {
            _cache = new Dictionary<Type, IShareable>();
        }
        public Cache(IDictionary<Type, IShareable> cache)
        {
            _cache = cache;
        }
    }
}

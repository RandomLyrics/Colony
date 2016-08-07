using System;
using System.Collections.Generic;

namespace Orion.Engine
{
    public class Cache
    {
        private IDictionary<Type, IShareable> _cache;
        public IDictionary<Type, IShareable> Dic { get { return _cache; } }

        public IShareable this[Type x]
        {
            get { return _cache[x]; }
            set { _cache[x] = value; }
        }

        public bool ContainsKey<T>() { return (_cache.ContainsKey(typeof(T))); }
        public bool ContainsKey(Type type) { return (_cache.ContainsKey(type)); }

        public Cache()
        {
            _cache = new Dictionary<Type, IShareable>();
        }
    }
}

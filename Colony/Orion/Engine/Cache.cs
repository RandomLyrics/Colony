﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Engine
{
    public class Cache
    {
        private IDictionary<Type, IShareable> _cache;
        public IDictionary<Type, IShareable> Dic { get; }

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
        public Cache(IDictionary<Type, IShareable> cache)
        {
            _cache = cache;
        }

        //public void Inject<T>(T obj)

        //public void Insert<T>(ref T obj) where T : new()
        //{
        //    // if (!_cache.ContainsKey(typeof(T)))
        //    //  {
        //    obj = new T();
        //    this._cache[typeof(T)] = (IShareable)obj;
        //    // }

        //}
    }
}

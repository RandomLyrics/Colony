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
            this.Before();
            //this.InjectProperties();
        }

        protected void InjectProperties<T>(T obj)
        {
            Injector.LoadProperties<T>();
            Injector.GenerateConstructors<T>();
            Injector.FillClass<T>(obj);
        }

        //public Shared(Cache cache)
        //{
        //    this._cache = cache;
        //}

        //METHODS
        public virtual void Before() { }

        public void ToCache(Cache cache)
        {
            this._cache = cache;
        }

        protected void PropertiesToCache<TBase>(TBase obj, Cache cache)
        {
            if (!Cache.ContainsKey<TBase>())
            {

            }
           // Injector.Inject(this, Cache);
        }
    }
}

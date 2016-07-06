using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            //this.Before();
            //var asd = this.GetType();

            //this
            //getproperties
            //for prop 
            //  if cache[proptype] != null -> prop.value = cache[proptype]
            //  else prop.value = new prop.type() { cache = this.cache }


            //this.InjectProperties();
        }
        public T Build<T>() where T: Shared
        {
            BuildHierarchy();
            return (T)this;
        }
        public void BuildHierarchy()
        {
            var props = Hierarchy.GetProperties(this);
            foreach (var prop in props)
            {
                if (!Cache.ContainsKey(prop.PropertyType))
                {
                    //dynamic ctor = Activator.CreateInstance(prop.PropertyType);
                    IShareable ctor = Hierarchy.New(prop.PropertyType);
                    Cache[prop.PropertyType] = ctor;
                    ctor.Cache = this.Cache;
                    ctor.BuildHierarchy();
                }
               // var setter = Injector.GetSetter(prop);
                prop.SetValue(this, Cache[prop.PropertyType]);
            }
            //this.Cache = cache;

        }

        protected void Construct(Type type)
        {
            var ctor = Expression.New(type);
            
           // Injector.LoadProperties<T>();
           // Injector.GenerateConstructors<T>();
            //Injector.FillClass<T>(obj);
        }

        protected void FillProperties<T>(T obj) where T: Shared, new()
        {
            Func<T> del = () => new T();
            Expression<Func<T>> expNew = () => new T();
            Expression<Action<T, Cache>> exp = (x, c) => x.ToCache(c);
            var ee = exp;
            exp.Compile()(obj, this.Cache);
            var asd = obj;
            
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

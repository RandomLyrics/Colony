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

        //METHODS
        public T Build<T>() where T: Shared
        {
            lock (_cache)
            {
                BuildHierarchy();
                return (T)this;
            }
        }
        public void BuildHierarchy()
        {
            var props = Hierarchy.GetProperties(this);
            foreach (var prop in props)
            {
                if (!Cache.ContainsKey(prop.PropertyType))
                {
                    IShareable ctor = Hierarchy.New(prop.PropertyType);
                    Cache[prop.PropertyType] = ctor;
                    ctor.Cache = this.Cache;
                    ctor.BuildHierarchy();
                }
               // Hierarchy.SetValue(this, Cache[prop.PropertyType]);
                prop.SetValue(this, Cache[prop.PropertyType]);
                this.After();
            }
        }

        //EVENTS
        protected virtual void Before() { }
        protected virtual void After() { }
    }
}

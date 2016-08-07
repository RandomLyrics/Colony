using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Orion.Engine
{
    public static class Hierarchy
    {
        //private static IEnumerable<PropertyInfo> _Properties = new List<PropertyInfo>();
        private static IDictionary<Type, IEnumerable<PropertyInfo>> _classProperties = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        private static IDictionary<Type, Func<IShareable>> _classConstructors = new Dictionary<Type, Func<IShareable>>();
        private static IDictionary<Type, IShareable> _builded = new Dictionary<Type, IShareable>();
        private static IDictionary<Type, PropertyInfo> _classSetters = new Dictionary<Type, PropertyInfo>();
        


        //METHODS
        public static void Build<T>(T domain) where T: Shared
        {
            //lock (_builded)
            //{
            //    if (!_builded.ContainsKey(typeof(T)))
            //    {
            // _builded[typeof(T)] = domain.Build<T>();
            domain.Build<T>();
            //    }
            //    else
            //    {
            //        domain = (T)_builded[typeof(T)];
            //    }
            //}

        }

        internal static void SetValue(IShareable instance, IShareable prop)
        {
           // if (!_classSetters.Any(x=>x.Key == ))
        }

        internal static IEnumerable<PropertyInfo> GetProperties(IShareable obj)
        {
            lock (_classProperties)
            {
                var type = obj.GetType();
                if (!_classProperties.ContainsKey(type))
                {
                    _classProperties[type] = type.GetProperties().Where(p => (typeof(IShareable).IsAssignableFrom(p.PropertyType)));
                }
                return _classProperties[type];
            }
            
        }

        internal static IShareable New(Type type)
        {
            lock (_classConstructors)
            {
                if (!_classConstructors.ContainsKey(type))
                {
                    var ctor = Expression.New(type);
                    var expr = Expression.Lambda<Func<IShareable>>(ctor);
                    _classConstructors[type] = expr.Compile();
                }
                return _classConstructors[type]();
            }
            
           
        }
    }
}

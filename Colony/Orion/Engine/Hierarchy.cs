using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Orion.Example;
using System.Linq.Expressions;
using System.Threading;

namespace Orion.Engine
{
    public static class Hierarchy
    {
        static Func<int, int> adsd = (a) => { return a; };
        //GENERICS
        public static void IfDoubleLock(object locker, Func<bool> pred, Action expr)
        {
            if (pred())
            {
                lock (locker)
                {
                    if (pred())
                        expr();
                }
            }
        }

        //GET PROPERTIES
        private readonly static IDictionary<Type, IEnumerable<PropertyInfo>> _classProperties = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        internal static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            if (!_classProperties.ContainsKey(type))
                _classProperties[type] = type.GetProperties().Where(p => (typeof(IShareable).IsAssignableFrom(p.PropertyType)));
            return _classProperties[type];
        }

        //GET CONSTRUCTOR
        private static IDictionary<Type, Func<IShareable>> _classConstructors = new Dictionary<Type, Func<IShareable>>();
        private static object _CCGuard = new object();
        internal static IShareable New(Type type)
        {
            IfDoubleLock(_CCGuard
                ,()=> !_classConstructors.ContainsKey(type)
                ,()=> {
                    var expr = Expression.Lambda<Func<IShareable>>(Expression.New(type));
                    _classConstructors[type] = expr.Compile();
                });
            return _classConstructors[type]();
        }


        private static IList<Cache> _cachePool = new List<Cache>();

        internal static void BuildHierarchy(IShareable obj)
        {
            var cache = new Cache();
            Build(obj, cache);
            
        }
        internal static void Build(IShareable obj, Cache cache)
        {
            foreach (var prop in GetProperties(obj.GetType()))
            {
                if (!cache.ContainsKey(prop.PropertyType))
                {
                    IShareable shareable = New(prop.PropertyType);
                    cache[prop.PropertyType] = shareable;
                    Build(shareable, cache);
                }
                SetPropertyValue(obj, prop, cache[prop.PropertyType]);
                prop.SetValue(obj, cache[prop.PropertyType]);
            }
        }

        private static void SetPropertyValue<T>(T obj, PropertyInfo prop, IShareable shareable) where T : IShareable
        {
            throw new NotImplementedException();
        }



        //private static IDictionary<Type, IShareable> _cached = new Dictionary<Type, IShareable>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Engine
{
    public static class Injector
    {
        //private static IEnumerable<PropertyInfo> _Properties = new List<PropertyInfo>();
        private static IDictionary<Type, IEnumerable<PropertyInfo>> _classProperties = new Dictionary<Type, IEnumerable<PropertyInfo>>();

        public static void Inject<T>(T obj, Cache cache)
        {
            var type = obj.GetType();
            if (!_classProperties.ContainsKey(type))
                _classProperties[type] = type.GetProperties().Where(p => typeof(IShareable).IsAssignableFrom(p.PropertyType));

            if (cache == null)
            {
                foreach (var prop in _classProperties[type])
                {
                    prop.SetValue(obj, Activator.CreateInstance(type));
                }
            } else
            {
                foreach (var prop in _classProperties[type])
                {
                    prop.SetValue(obj, Convertt(Activator.CreateInstance(prop.PropertyType.BaseType, cache), prop.PropertyType));
                }
            }
        }

        public static dynamic Convertt(dynamic source, Type dest)
        {
            return Convert.ChangeType(source, dest);
        }
    }
}

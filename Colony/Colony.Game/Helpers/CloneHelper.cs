using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Colony.Game.Helpers
{
    using Colony = Colony.Game.Models.Colony;

    public static class CloneHelper
    {
        public static T Clone<T>(this T obj) where T : class, new()
        {
            T copied = new T();
            var type = obj.GetType();
            var props = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            foreach (var prop in props)
            {
                prop.SetValue(copied, prop.GetValue(obj));
            }
            return copied;
        }
    }
}

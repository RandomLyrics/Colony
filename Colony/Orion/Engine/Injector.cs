using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Engine
{
    public static class Injector
    {
        //private static IEnumerable<PropertyInfo> _Properties = new List<PropertyInfo>();
        private static IDictionary<Type, IEnumerable<PropertyInfo>> _classProperties = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        private static IDictionary<Type, Func<Cache, IShareable>> _classConstructors = new Dictionary<Type, Func<Cache, IShareable>>();
        

        public static void LoadProperties<T>()
        {
            var type = typeof(T);
            if (!_classProperties.ContainsKey(type))
                _classProperties[type] = type.GetProperties().Where(p => typeof(IShareable).IsAssignableFrom(p.PropertyType));

            //if (cache == null)
            //{
            //    foreach (var prop in _classProperties[type])
            //    {
            //        prop.SetValue(obj, CreateNew(obj, prop, cache));
            //    }
            //} else
            //{
            //    foreach (var prop in _classProperties[type])
            //    {
            //        prop.SetValue(obj, CreateNew(obj, prop, cache));
            //    }
            //}
        }

        internal static void FillClass<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public static void GenerateConstructors<T>() where T: Shared, new()
        {
            foreach (var prop in _classProperties[typeof(T)])
            {
                Expression<Func<T, IShareable>> exp = (obj) => obj = new T();

                var ctor = Expression.New(prop.PropertyType);
                //var sad = DynamicExpression.ParseLambda
                //var cccc = NewArrayExpression.NewArrayBounds(prop.PropertyType, Expression.Assign(Expression.Property()))
                var obj = Expression.Constant(prop.PropertyType);
                var property = Expression.Property(obj, "Cache");
                var elder = Expression.Parameter(typeof(T), typeof(T).Name);
                var caheprop = Expression.Property(elder, "Cache");
                var assign = Expression.Assign(property, caheprop);

                var block = Expression.Block(ctor, assign, obj);
                var expr = Expression.Lambda<Func<IShareable>>(ctor);

                var asds = expr.Compile()();
                //_classConstructors[type.PropertyType] = 
            }
            //var type = typeof(T);
            
        }

        public static IShareable CreateNew<T>(T obj, PropertyInfo prop, Cache cache)
        {
            var type = obj.GetType();
            var CacheInput = Expression.Parameter(typeof(Cache), "Cache");
            var ctor = Expression.New(type);
            var local = Expression.Parameter(type, "objec");
            var CacheProperty = Expression.Property(local, "Cache");

            var returnTarget = Expression.Label(type);
            var returnExpression = Expression.Return(returnTarget, local, type);
            var returnLabel = Expression.Label(returnTarget, Expression.Default(type));

            var block = Expression.Block(
                new[] { local },
                Expression.Assign(local, ctor),
                Expression.Assign(CacheProperty, CacheInput),
                returnExpression,
                returnLabel);

            _classConstructors[type] = Expression.Lambda<Func<Cache, IShareable>>(block, CacheInput).Compile();

            return null;
        }


        public static Func<bool, dynamic> Creator;

        static void BuildLambda()
        {
            var expectedType = typeof(object);
            var displayValueParam = Expression.Parameter(typeof(bool), "displayValue");
            var ctor = Expression.New(expectedType);
            var local = Expression.Parameter(expectedType, "obj");
            var displayValueProperty = Expression.Property(local, "DisplayValue");

            var returnTarget = Expression.Label(expectedType);
            var returnExpression = Expression.Return(returnTarget, local, expectedType);
            var returnLabel = Expression.Label(returnTarget, Expression.Default(expectedType));

            var block = Expression.Block(
                new[] { local },
                Expression.Assign(local, ctor),
                Expression.Assign(displayValueProperty, displayValueParam),
                /* I forgot to remove this line:
                 * Expression.Return(Expression.Label(expectedType), local, expectedType), 
                 * and now it works.
                 * */
                returnExpression,
                returnLabel
                );
            Creator =
                Expression.Lambda<Func<bool, dynamic>>(block, displayValueParam)
                    .Compile();
        }
    }
}

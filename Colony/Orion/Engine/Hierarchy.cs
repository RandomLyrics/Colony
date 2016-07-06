using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Engine
{
    public static class Hierarchy
    {
        //private static IEnumerable<PropertyInfo> _Properties = new List<PropertyInfo>();
        private static IDictionary<Type, IEnumerable<PropertyInfo>> _classProperties = new Dictionary<Type, IEnumerable<PropertyInfo>>();
        private static IDictionary<Type, Func<IShareable>> _classConstructors = new Dictionary<Type, Func<IShareable>>();
        private static IDictionary<Type, IShareable> _builded = new Dictionary<Type, IShareable>();
        

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

        internal static void Build<T>(T domain) where T: Shared
        {
            if (!_builded.ContainsKey(typeof(T)))
            {
                _builded[typeof(T)] = domain.Build<T>();
            }else
            {
                domain = (T)_builded[typeof(T)];
            }
        }

        internal static IEnumerable<PropertyInfo> GetProperties(IShareable obj)
        {
            var type = obj.GetType();
            if (!_classProperties.ContainsKey(type))
            {
                _classProperties[type] = type.GetProperties().Where(p => (typeof(IShareable).IsAssignableFrom(p.PropertyType)));
            }
            return _classProperties[type];
        }

        internal static Action<IShareable, IShareable> GetSetter(PropertyInfo prop)
        {
            //return null;
            var instance = Expression.Parameter(prop.ReflectedType, "instance");
            var property = Expression.Parameter(prop.PropertyType, "property");
            var call = Expression.Call(instance, prop.GetSetMethod(), property);
            //var parameters = new ParameterExpression
            return null;
        }

        internal static IShareable New(Type type)
        {
            if (!_classConstructors.ContainsKey(type))
            {
                var ctor = Expression.New(type);
                var expr = Expression.Lambda<Func<IShareable>>(ctor);
                _classConstructors[type] = expr.Compile();
            }
            return _classConstructors[type]();
        }

        internal static void FillClass<T>(T obj)
        {
            throw new NotImplementedException();
        }

        public static void GenerateConstructors<T>() where T: Shared, new()
        {
            foreach (var prop in _classProperties[typeof(T)])
            {
                //Expression<Func<T, IShareable>> exp = (x) => x.Cache = ;

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

            //_classConstructors[type] = Expression.Lambda<Func<Cache, IShareable>>(block, CacheInput).Compile();

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

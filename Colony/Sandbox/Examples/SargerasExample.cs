using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sandbox.Examples
{
    public static class Types
    {
        public const string Int = "System.Int32";
        public const string String = "System.String";
        public const string Decimal = "System.Decimal";
    }
    public class DynamicClass: INotifyPropertyChanged
    {
        public Dictionary<string, Type> Properties { get; set; } = new Dictionary<string, Type>();

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        public void AddProp(string name, string type)
        {
            Properties[name] = Type.GetType(type);
        }
    }

    public class DynamicInstance : IDynamicMetaObjectProvider
    {
        private DynamicClass Schema;
        public int Id { get; set; }
        public Dictionary<string, object> Properties { get; set; } = new Dictionary<string, object>();

        public DynamicInstance(DynamicClass schema)
        {
            Schema = schema;
        }

        public DynamicInstance()
        {
        }

        public T Get<T>(string propname)
        {
            if (!(Schema.Properties[propname] == typeof(T)))
                throw new Exception("Wrong Type, that property has other type");
            return (T)Properties[propname];
        }
        public void Set(string propname, object val)
        {
            if (!(Schema.Properties[propname] == val.GetType()))
                throw new Exception("Wrong Type, that property has other type");
            Properties[propname] = val;
        }

        DynamicMetaObject IDynamicMetaObjectProvider.GetMetaObject(Expression parameter)
        {
            var ad = parameter;
            return null;
        }
        private void Id_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Console.WriteLine("A property has changed: " + e.PropertyName);
        }
    }


    public class SargerasExample
    {
        public void Run()
        {
            var d3 = new DynamicInstance();
            d3.Id = 23;
            //d3.GetType().GetProperties().First().set
            int[] dd = new int[0];
            //dd.whe
            new RoboOpierdalacz().Run();
            var dc = new DynamicClass();
            dc.AddProp("Id", Types.Int);
            dc.AddProp("Name", Types.String);
            dc.AddProp("Money", Types.Decimal);
            //dynamic dupa = new DynamicInstance(dc);
            //dupa.Lol = 22;
            //dynamic ss = new ExpandoObject();
            //ss.Dupa = 33;
            //var asd = (int)ss.Dupa;
            //Expression dsad = Exp
            for (int i = 0; i < 1524856; i++)
            {
                
                Console.WriteLine("id: " + i);
                Thread.Sleep(2000);
            }

            var di = new DynamicInstance(dc);
            di.Set("Id", 123123);
            var xx = di.Get<int>("Id");

        }
    }
}

using Hermes;
using Newtonsoft.Json;
using OrionOld.Engine;
using OrionOld.Example;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OrionOld
{
    public class Minidupa
    {
        public string mnull;
        public int blabla { get; set; } = 5;
    }
    public class Dupa
    {
        public int INTT { get; set; } = 1;
        public List<Minidupa> Minis { get; set; } = new List<Minidupa>();
    }
    class Program
    {
        public static  async void Run(int i)
        {
            Console.WriteLine("started");
            await DoSomething(i).ConfigureAwait(false);
            Console.WriteLine("finished " + i);
        }

        private static Task DoSomething(int i)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(1000);
                Console.WriteLine("done something " + i);
            });
           
            //return Run();
        }
        public static int Call(int i, Func<int, int> func)
        {
            return func(i);
        }

        public static Action<Domain, Data> ffunc = (dom, da) => dom.Data = da;
        public static Action<object, object> dfunc(object obj, object val)
        {
            var field = obj.GetType().GetProperty("Data");
            ParameterExpression targetExp = Expression.Parameter(typeof(object), "target");
            ParameterExpression valueExp = Expression.Parameter(typeof(object), "value");

            // Expression.Property can be used here as well
            MemberExpression fieldExp = Expression.Property(targetExp, field);
            BinaryExpression assignExp = Expression.Assign(fieldExp, valueExp);

            var setter = Expression.Lambda<Action<object, object>>
                (assignExp, targetExp, valueExp).Compile();

            return setter;
        }


        static void Main(string[] args)
        {
            //var dupa = new Dupa();
            //dupa.Minis.Add(new Minidupa());
            //dupa.Minis.Add(new Minidupa());

            //var ss = JsonConvert.SerializeObject(dupa);
            //var ad = Stats.SizeOf(() => { return new Domain(); }, false, false);
            //var method = typeof(Domain).GetProperty("Data").GetSetMethod();
            //var prop = typeof(Domain).GetProperty("Data");
            //var data = new Data();
            //var dom = new Domain();
            //var methodd = dfunc(dom, data);

            //using (new SpeedTimer())
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
                    
            //        method.Invoke(dom, new object[] { data });
            //    }
            //}
            //using (new SpeedTimer())
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        methodd(dom, data);
            //    }
            //}
            //using (new SpeedTimer())
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        prop.SetValue(dom, data);
            //    }
            //}
            //using (new SpeedTimer())
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        dom.Data = data;
            //    }
            //}
            //Action<Domain, Data> fffunc = (domm, da) => domm.Data = da;
            //using (new SpeedTimer())
            //{
            //    for (int i = 0; i < 100; i++)
            //    {
            //        fffunc(dom, data);
            //    }
            //}
            ////Run(1);
            ////Run(2);
            ////Run(3);
            ////Console.ReadKey();
            //Func <int, int > ff = ((i) => {  return i; });
            //ff(4);
            //using (new SpeedTimer())
            //{
            //    for (int i = 0; i < 1; i++)
            //    {
            //        var sss = Call(i, ff);
            //    }
            //}
            //using (new SpeedTimer())
            //{
            //    for (int i = 0; i < 1; i++)
            //    {
            //        var ads = i;
            //    }
            //}
            //using (new SpeedTimer())
            //{
            //    for (int i = 0; i < 1; i++)
            //    {
            //        var sss = Call(i, ff);
            //    }
            //}

            ////var ss = new List<string>();
            ////ss.Add("sadasd");
            ////foreach (var item in ss)
            ////{
            ////    ss.Add("asdasd");
            ////}
        }
    }
}

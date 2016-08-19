using Hermes;
using Sandbox.DynamicLambda.Example;
using Sandbox.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class Wave
    {
        public int height = 0;
    }
    struct SObject
    {
        public Wave val;
        public SObject(Wave _val)
        {
            val = _val;
        }
    }
    class Program
    {
        static int amount = 1000000;
        static void Main(string[] args)
        {
            new XMLparsero().Run();

            //Random r = new Random();
            //List<Wave> wavelist = new List<Wave>(amount);
            //List<SObject> stacklist = new List<SObject>();
            //Action wave = () =>
            //{
            //    wavelist.Add(new Wave());
            //    wavelist.Last().height = r.Next(1000000);
            //};
            //Action stack = () =>
            //{
            //    stacklist.Add(new SObject(new Wave()));
            //    stacklist.Last().val.height = r.Next(1000000);
            //};

            //Stats.PrintOutTicks(wave, amount);
            //Stats.PrintOutTicks(stack, amount);
            //Stats.PrintOutTicks(wave, amount);
            //Stats.PrintOutTicks(stack, amount);
            //var dupa = 50;
            //new ExampleRun().Run();
            //new OperatorSQLExample().Run();
        }
    }
}

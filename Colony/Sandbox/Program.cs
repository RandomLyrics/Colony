
using CSVBot;
using Hermes;
using Sandbox.DynamicLambda.Example;
using Sandbox.Examples;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

    public class LogEventEntry
    {
        public int EventId { get; set; }
        public string Entity { get; set; }
        public int Severity { get; set; }
    }
    public class LogEventModel
    {
        public string Name { get; set; }
        public int Severity { get; set; }
        public int Count { get; set; }
        public double Perc { get; set; }
    }
    class Program
    {
        
        static int amount = 1000000;
        static void Main(string[] args)
        {

            new OperatorSQLExample().Run();

            return;

            Random rnd = new Random();
            var entities = new List<string>() { "LPostTerm", "AmexBase", "PKOBase" };
            var import = new List<LogEventEntry>();
            for (int i = 0; i < 5000; i++)
            {
                import.Add(new LogEventEntry() { EventId = rnd.Next(8), Entity = entities[rnd.Next(3)], Severity = 0 });
            }
            
            CalculateLog(import);
            CalculateLog(import.Take(2500).ToList());
            CalculateLog(import.Take(500).ToList());




            var tmp = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    tmp = ip.ToString();
                }
            }

            IPHostEntry hostEntry = Dns.GetHostEntry(tmp);

            var machineName = hostEntry.HostName;

            CSV.SaveToFile(null, @"CODE.csv");

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

        private static void CalculateLog(List<LogEventEntry> import)
        {
            var look = import.ToLookup(x => x.EventId);
           // look.fr
            var r = look.Select(x => new LogEventModel()
            {
                Name = "event" + x.Key,
                Count = x.Count(),
                Perc = x.Count() / (double)import.Count * 100,
                Severity = x.First().Severity
            }).OrderByDescending(x => x.Perc).ToList();
            foreach (var x in r)
            {
                Console.WriteLine(x.Name + ": " + x.Count + " " + x.Perc + " - " + x.Severity);
            }
        }

    }
}

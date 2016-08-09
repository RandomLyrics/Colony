using Sandbox.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.DynamicLambda.Example
{
    public class ExampleRun
    {
        private const string _ISOFile = @"CountryA3.csv";
        private const string _table = "Dictionary.Issuer";
        private const string _column = "Country";
        private List<string> _query = new List<string>();

        public void Run()
        {
            //var dic = new Dictionary<int, Task>();
            //var que = new Queue<Task>();
            //var b1 = GC.GetTotalMemory(false);
            //Console.WriteLine(b1);
            //var ss = new Dictionary<string, long>();
            //for (int i = 0; i < 10000; i++)
            //{
            //    ss[i.ToString()] = i % 100;
            //}
            //Console.WriteLine("dictionary");
            //Console.WriteLine(GC.GetTotalMemory(false) - b1);
            //Console.WriteLine("  ");
            //b1 = GC.GetTotalMemory(false);
            //Console.WriteLine(b1);
            //var dd = new List<string>();
            //for (int i = 0; i < 10000; i++)
            //{
            //    dd.Add((i * i).ToString());
            //}
            //Console.WriteLine("list");
            //Console.WriteLine(GC.GetTotalMemory(false) - b1);
            //Console.WriteLine("asdsad");
            var iso = CSV.Read(_ISOFile, ';').AsEnumerable();

            //var query = new List<string>();
            foreach (var a3 in iso)
            {
                AUp(a3["name"].ToString(), a3["A3"].ToString());
            }
            _query.Add("--------------------NOT ISO RULES--------------------");
            AUp("Hong Kong, China", "HKG");
            AUp("Ireland", "IRL");
            AUp("South Korea", "KOR");
            AUp("Taiwan", "TWN");
            AUp("Polska", "POL");
            AUp("United States", "USA");
            AUp("Lietuva", "LTU");
            AUp("Shanghai", "CHN");
            AUp("Luxembourg", "LUX");
            AUp("Russia", "RUS");
            AUp("Republic of Ireland", "IRL");
            AUp("Belgie", "BEL");
            AUp("the netherlands", "NLD");
            AUp("brasil", "BRA");
            AUp("argentine", "ARG");
            AUp("st.vincent", "VCT");
            AUp("malesia", "MYS");
            //Da("kosovo", )
            AUp("singapur", "SGP");
            AUp("england", "GBR");
            AUp("china", "CHN");
            AUp("islandia", "ISL");

            //
            File.WriteAllLines("DICountryISOUpdate.sql", _query.ToArray());
        }

        public void AUp(string from, string to)
        {
            if (from.Contains("'"))
                from = from.Replace("'", "");
            _query.Add("UPDATE " + _table);
            _query.Add("SET " + _column + '=' + Q(to));
            _query.Add("WHERE " + _column + '=' + Q(from));
            _query.Add("   ");
        }

        public string Q(string word)
        {
            return "'" + word + "'";
        }
    }
}

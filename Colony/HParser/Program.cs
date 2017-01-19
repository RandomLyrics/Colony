using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace HParser
{
    class Program
    {
        static string _filepath = @"testA.txt";

        static string _hrules = @"1 4 RecID
5 6 DateM
7 8 NumberX";
        static string _drules = @"1 2 PropID
3 4 LengthX
5 8 DataX
9 10 FlagX";
        static string _trules = @"1 4 RecID
5 7 CountX";

        static void Main(string[] args)
        {
            if (args[0] == "setup")
            {
                var inputPath = ConfigurationManager.AppSettings["InputPath"];
                var outputPath = ConfigurationManager.AppSettings["OutputPath"];

                if (!Directory.Exists(inputPath))
                    Directory.CreateDirectory(inputPath);
                if (!Directory.Exists(outputPath))
                    Directory.CreateDirectory(outputPath);


                //var dd = new SqlDB()
                List<string> gatewayNames = DB.
            }
            var ss = "1 2 GG;3 4 AA;".Split(';');
            var aaa = ss[1].Split(' ');

            var hrules = _hrules.Split('\n');
            var drules = _drules.Split('\n');
            var trules = _trules.Split('\n');

            var bulk = new Dictionary<string, Collection<Dictionary<string, string>>>();
            bulk.Add("Headers", new Collection<Dictionary<string, string>>());
            bulk.Add("Details", new Collection<Dictionary<string, string>>());
            bulk.Add("Trailers", new Collection<Dictionary<string, string>>());

            using (var sr = new StreamReader(_filepath))
            {

                while (!sr.EndOfStream)
                {
                    var l = sr.ReadLine();
                    if (l[0] == 'H')
                    {
                        bulk["Headers"].Add(ParseLine(hrules, l));
                    }
                    else
                    if (l[0] == 'D')
                    {
                        bulk["Details"].Add(ParseLine(drules, l ));
                    }
                    else
                    if (l[0] == 'T')
                    {
                        bulk["Trailers"].Add(ParseLine(trules, l));
                    }
                }
                
            }
            var c = 0;

        }

        private static Dictionary<string, string> ParseLine(string[] rules, string l)
        {
            var dic = new Dictionary<string, string>();
            foreach (var item in rules)
            {
                var r = item.Split(' ');
                var eval = Calc(l, r);
                dic.Add(r[2], eval);
            }
            return dic;
        }

        private static string Calc(string l, string[] r)
        {
            var x = int.Parse(r[0]);
            var y = int.Parse(r[1]);
            var c = y - x + 1;
            return l.Substring(x, c);
        }
    }
}

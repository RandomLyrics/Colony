using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVBot
{
    //sadasdsad
    public static class CSV
    {
        public static void SaveToFile(IEnumerable<object> objs, string path, bool append = false, Encoding encoding = null)
        {
            using (var sw = new StreamWriter(path, append, encoding ?? Encoding.Unicode))
            {
                sw.WriteLine("Id|Char");
                for (int i = 0; i < Int16.MaxValue; i++)
                {
                    var s = Char.ConvertFromUtf32(i);
                    sw.WriteLine(String.Format("{0}|{1}", i, s));
                }
            }
        }
    }
}

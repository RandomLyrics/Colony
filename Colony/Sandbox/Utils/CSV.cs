using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Utils
{
    public static class CSV
    {
        public static void Write(string opath, List<DataRow> list, string[] columns, char separator = ';')
        {
            if (!list.Any())
                return;
            try
            {
                using (StreamWriter sw = new StreamWriter(opath, false, Encoding.UTF8))
                {
                    var sb = new StringBuilder();
                    foreach (var col in columns)
                        sb.Append(col).Append(separator);
                    sb.Length--;
                    sw.WriteLine(sb.ToString());
                    sb.Clear();

                    foreach (var row in list)
                    {
                        foreach (var col in columns)
                            sb.Append(row[col].ToString()).Append(separator);
                        sb.Length--;
                        sw.WriteLine(sb.ToString());
                        sb.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:\n" + e);
                throw;
            }
        }
        public static void Write<T>(string opath, List<T> list, char separator)
        {
            if (!list.Any())
                return;
            try
            {
                var props = list[0].GetType().GetProperties();
                using (StreamWriter sw = new StreamWriter(opath))
                {
                    var sb = new StringBuilder();
                    foreach (var col in props)
                        sb.Append(col.Name).Append(separator);
                    sb.Length--;
                    sw.WriteLine(sb.ToString());
                    sb.Clear();

                    foreach (var item in list)
                    {
                        foreach (var prop in props)
                            sb.Append(prop.GetValue(item).ToString()).Append(separator);
                        sb.Length--;
                        sw.WriteLine(sb.ToString());
                        sb.Clear();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error during CSV writing:\n" + e);
            }
        }

        public static DataTable Read(string path, char separator)
        {
            var table = new DataTable();
            table.Clear();
            try
            {
                //string[] columns;
                //string[] elements;
                //using (TextReader rd = File.OpenText(path))
                //{
                //    columns = rd.ReadLine().Split(separator);
                //    elements = rd.ReadToEnd().Split(separator);
                //}
                //foreach (var item in columns)
                //    table.Columns.Add(item);

                //var counter = 0;
                //do
                //{
                //    DataRow row = table.NewRow();
                //    for (int i = 0; i < columns.Length; i++)
                //    {
                //        row[columns[i]] = elements[counter++];
                //    }
                //    table.Rows.Add(row);
                //} while (counter < elements.LongLength-1);

                //DataRow row = table.NewRow();
                //var col = 0;
                //for (int i = 0; i < elements.LongLength; i++)
                //{
                //    row[columns[col]] = elements[i];
                //    col++;
                //    if ( i > 0 && columns.Length-1 % i == 0)
                //    {
                //        table.Rows.Add(row);
                //        row = table.NewRow();
                //    }

                //}
                //var count = elements.Length;
                //var zero = (double)elements.Length / (double)columns.Length;
                //var textt = File.ReadAllText(path, Encoding.UTF8).Trim();
                //var lines = textt.Split(separator);
                var lines = File.ReadAllLines(path, Encoding.UTF8);
                foreach (var item in lines[0].Split(separator))
                {
                    table.Columns.Add(item);
                }
                var c = table.Columns;
                for (int i = 1; i < lines.Length; i++)
                {
                    DataRow ro2w = table.NewRow();
                    var data = lines[i].Split(separator);
                    for (int k = 0; k < c.Count; k++)
                    {
                        ro2w[c[k]] = data[k].Trim();
                    }
                    table.Rows.Add(ro2w);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while reading csv:\n" + e);
                throw;
            }
            return table;
        }
    }
}

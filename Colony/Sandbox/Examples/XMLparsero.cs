using Hermes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sandbox.Examples
{
    #region ARCH

    //using (new SpeedTimer())
    //{
    //    for (int i = 0; i < 10000; i++)
    //    {
    //        list2.Add(new DBRecord()
    //        {
    //            Id = i,
    //            ICA = r.Next(100000, 400000).ToString(),
    //            BIN = Flip(90) ? tbin = r.Next(400000, 600000).ToString() : tbin,
    //            Adress = Flip(80) ? RandomString(10) : null,
    //            City = Flip(50) ? RandomString(8) : null,
    //            Country = Flip(60) ? RandomString(9) : null,
    //            Name = Flip(95) ? RandomString(15) : null,
    //            State = Flip(30) ? RandomString(5) : null
    //        });
    //    }
    //}
    //using (new SpeedTimer())
    //{
    //    list.ForEach(x, i) =>
    //         new DBRecord()
    //        {
    //            Id = i,
    //            ICA = r.Next(100000, 400000).ToString(),
    //            BIN = Flip(90) ? tbin = r.Next(400000, 600000).ToString() : tbin,
    //            Adress = Flip(80) ? RandomString(10) : null,
    //            City = Flip(50) ? RandomString(8) : null,
    //            Country = Flip(60) ? RandomString(9) : null,
    //            Name = Flip(95) ? RandomString(15) : null,
    //            State = Flip(30) ? RandomString(5) : null
    //        });
    //}

    #endregion
    //public Dictionary< MyProperty { get; set; }
    public class DBRecord
    {
        public int Id { get; set; }
        public string ICA { get; set; }
        public string BIN { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
    }
    public class RolePage
    {
        public string Title { get; set; }
        public List<string> Roles { get; set; }
        public RolePage Child { get; set; }
    }
    public class XMLparsero
    {
        static Random r = new Random();
        string xmlpath = @"Web.sitemap";

        public void Bla<T>() where T: class, new()
        {
            //var ss = Convert.ChangeType;
            Expression<Func<T>> dd = ()=> new T();
            var aa = dd.Compile();
            var asd = aa();
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[r.Next(s.Length)]).ToArray());
        }
        public bool Flip(int chance)
        {
            return r.Next(0, 100) <= chance;
        }

        public void CleanTable()
        {
            Bla<RolePage>();
            var list = new List<DBRecord>();
            var tbin = Flip(95) ? r.Next(400000, 600000).ToString() : null;
            //generate
            using (new SpeedTimer())
            {
                for (int i = 0; i < 10000; i++)
                {
                    list.Add(new DBRecord()
                    {
                        Id = i,
                        ICA = r.Next(100000, 400000).ToString(),
                        BIN = Flip(90) ? tbin = r.Next(400000, 600000).ToString() : tbin,
                        Adress = Flip(80) ? RandomString(10) : null,
                        City = Flip(50) ? RandomString(8) : null,
                        Country = Flip(60) ? RandomString(9) : null,
                        Name = Flip(95) ? RandomString(15) : null,
                        State = Flip(30) ? RandomString(5) : null
                    });
                }
            }
            //process
            var grouped = list.GroupBy(x => x.BIN).Where(x=>x.Count() > 1 );

        }
        public void Run()
        {
            CleanTable();
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlpath);
            
            var d = doc;
        }

        public RolePage ConvertToRolePage(XmlNode node)
        {
            var rp = new RolePage();
            //if (node.ChildNodes.Count > 0 )
            //    rp.Child = ConvertToRolePage(
            return rp;
        }
    }

}

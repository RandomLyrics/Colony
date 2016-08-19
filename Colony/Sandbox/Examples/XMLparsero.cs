using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sandbox.Examples
{
    public Dictionary< MyProperty { get; set; }
    public class RolePage
    {
        public string Title { get; set; }
        public List<string> Roles { get; set; }
        public RolePage Child { get; set; }
    }
    public class XMLparsero
    {
        string xmlpath = @"Web.sitemap";
        public void Run()
        {
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

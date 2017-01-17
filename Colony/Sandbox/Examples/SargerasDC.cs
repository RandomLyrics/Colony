using Sargeras.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Examples
{
    public class SargerasDC
    {
        public void Run()
        {
            var props = new Dictionary<string, Type>();
            props["Id"] = typeof(long);
            props["Name"] = typeof(string);
            props["Money"] = typeof(decimal);

            var company = DynamicClass.New("Company", props);
            //company<int>["Id"] = 3;
            //company["Name"] = "Miodex";
            //company["Money"] = 15151.4548474;

            //var d = company["Name"].toString();

            var Company = new DynamicClass();
        }
    }
}

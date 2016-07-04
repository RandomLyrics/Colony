using Orion.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Example
{
    public class Domain: Shared
    {
        public Data Data { get; set; }
        public Logic Logic { get; set; }

        public int MyProperty { get; set; }

        public Domain()
        {
            FromOrion.Injec(this);
        }
    }
}

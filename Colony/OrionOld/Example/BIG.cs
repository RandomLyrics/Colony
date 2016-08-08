using OrionOld.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionOld.Example
{
    public class BIG : IShareable
    {
        public string dsd = "ASDADS";

        public double asds = 213213.213213;

        public Cache Cache { get; set; }

        public void BuildHierarchy()
        {
            throw new NotImplementedException();
        }
    }
   
}

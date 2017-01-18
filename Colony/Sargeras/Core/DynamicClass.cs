using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sargeras.Core
{
    public class DynamicClass
    {
        public IDictionary<string, object> _classProperties { get; set; }
        //public Type this[string x]<T>
        //{
        //    get { return _classProperties[x]; }
        //    set { _classProperties[x] = value; }
        //}

        public static object New(string className, Dictionary<string, Type> props)
        {
            throw new NotImplementedException();
        }
    }
}

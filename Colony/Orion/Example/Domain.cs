using Orion.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Example
{
    public class Context : IShareable
    {

    }

    public class Lepo<T> : IShareable
    {
        public Data Data { get; set; }
    }
    public class Repo<T> : IShareable
    {
        public Context Context { get; set; }
    }

    public class Logic : IShareable
    {
        public Lepo<int> User { get; set; }
        public Lepo<double> Comapny { get; set; }
    }
    public class Data : IShareable
    {
        public Context Context { get; set; }
        public Repo<int> User { get; set; }
        public Repo<double> Company { get; set; }
    }

    public class Domain : IShareable
    {
        public Data Data { get; set; }
        public Logic Logic { get; set; }

        public Domain()
        {
            Hierarchy.BuildHierarchy(this);

        }
    }
}

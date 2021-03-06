﻿using Orion.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
           
            Action<Domain> TrimAllData = (x) =>
            {
                x.Logic.ToString();
                x.Logic.ToString();
            };
            TrimAllData(new Domain());

            //deserializedObject.TrimStringProperties();
            //deserializedObject.PrimaryOwnerArray.ForEach(m => m.TrimStringProperties());
            //deserializedObject.DeploymentArray.ForEach(m => m.TrimStringProperties());
            //var doms = new IEnumerable<Domain>();
            var asa = new List<Domain>();
            asa.ForEach((x) =>
            {
                x.Data = null;
                x.Logic = null;
            });
            //Hierarchy.BuildHierarchy(this);
            var ads = new List<PropertyInfo>();
            var dd = Hierarchy.GetProperties(typeof(Data));
            dd = ads;
        }
    }
}

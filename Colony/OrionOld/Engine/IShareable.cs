﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrionOld.Engine
{
    public interface IShareable
    {
        Cache Cache { get; set; }
        void BuildHierarchy();
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shanghaiwalk.model
{
    public class POI
    {
        public long id { get; set; } 
        public double gpslng { get; set; }
        public double gpslat { get; set; }
        public string name { get; set; }
        public string samename { get; set; }
        public string content { get; set; }

    }


}

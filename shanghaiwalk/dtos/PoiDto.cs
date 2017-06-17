using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shanghaiwalk.dtos
{
    public class PoiDto
    {
        public string name { get; set; }
        public string samename { get; set; }
        public double gpslat { get; set; }
        public double gpslng { get; set; }
        public string content { get; set; }
    }
}

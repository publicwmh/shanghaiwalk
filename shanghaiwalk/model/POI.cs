using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shanghaiwalk.model
{
    public class POI
    {
        public int id { get; set; } 
        public double GpsLng { get; set; }
        public double GpsLat { get; set; }
        public string name { get; set; }
        public string samename { get; set; }
        public string content { get; set; }

    }


}

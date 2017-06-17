using Microsoft.Extensions.Logging;
using shanghaiwalk.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shanghaiwalk.Baiye
{
    public class POIService
    {
        private BaiYeContext _baiyecontext;
        private readonly ILogger _logger;

        public POIService(
            BaiYeContext baiyecontent,
            ILogger logger)
        {
           
            _baiyecontext = baiyecontent;
            _logger = logger;
        }

        public POI GetPoiByName(string  name)
        {
          return  _baiyecontext.POIs.Where(p => p.samename.Contains(name)).FirstOrDefault();
        }

    }
}

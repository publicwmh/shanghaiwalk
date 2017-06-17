using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using shanghaiwalk.dtos;
using shanghaiwalk.model;
using shanghaiwalk.option;
using shanghaiwalk.third;
using Snowflake;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shanghaiwalk.Controllers
{
    public class PoiController: Controller
    {
        private readonly BaiYeContext _baiyecontext;
        private readonly ILogger _logger;
        private readonly LocationHelper _locheper;
        private readonly BaiduApiOption baiduapiOption;
        public PoiController(IOptions<BaiduApiOption> optbaidu,
                               BaiYeContext baiyecontext,
                               ILogger<PoiController> logger)
        {
            _baiyecontext = baiyecontext;
            _logger = logger;
            _locheper = new LocationHelper(optbaidu.Value);

        }
        public IActionResult NewPoi()
        {
            return View();
        }
        [HttpGet("poi/info/{id}")]
        public IActionResult Info(long id)
        {
            var poi = _baiyecontext.POIs.Where(p => p.id == id).FirstOrDefault();
            return View(poi);
        }
        [HttpPost]
        public string SavePoi(PoiDto input)
        {
            //转换百度地址为GPS
            var regps = _locheper.Convert2GPS(input.gpslat, input.gpslng);

            POI poi = new POI();
            poi.id = new IdWorker(1,1).NextId();
            poi.name = input.name;
            poi.samename = input.samename;
            poi.content = input.content;
            poi.gpslat = regps[0];
            poi.gpslng = regps[1];
            _baiyecontext.POIs.Add(poi);
            _baiyecontext.SaveChanges();
            return "OK";
        }
    }
}

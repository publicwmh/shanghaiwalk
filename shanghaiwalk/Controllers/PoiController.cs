using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace shanghaiwalk.Controllers
{
    public class PoiController: Controller
    {
        public IActionResult NewPoi()
        {
            return View();
        }
    }
}

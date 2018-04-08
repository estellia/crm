using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAPI.Test.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult CallWebAPIByBackend()
        {
            //return View();

            return View("CallWebAPIByBackend");
        }
    }
}
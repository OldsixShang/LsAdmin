using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example.Mvc.Controllers
{
    public class TouchController : Controller
    {
        // GET: Touch
        public ActionResult Index()
        {
            return View("~/Touch/layout.cshtml");
        }
    }
}
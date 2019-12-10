using IT.Web.MISC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT.Web.Controllers
{
    [Autintication]
    public class AdminHomeController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }
    }
}

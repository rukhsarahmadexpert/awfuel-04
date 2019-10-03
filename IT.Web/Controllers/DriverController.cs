using IT.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IT.Web.Controllers
{
    public class DriverController : Controller
    {
        public ActionResult Index()
        {
            return View ();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DriverViewModel driverViewModel)
        {
            return View();
        }
    }
}

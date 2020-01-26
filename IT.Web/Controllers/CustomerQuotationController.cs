using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT.Core.ViewModels;

namespace IT.Web.Controllers
{
    public class CustomerQuotationController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(LPOInvoiceViewModel lPOInvoiceViewModel)
        {
            return View();
        }

        public ActionResult Edit(int Id)
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ActionResult Update(LPOInvoiceViewModel lPOInvoiceViewModel)
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
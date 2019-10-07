using IT.Core.ViewModels;
using IT.Repository.WebServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IT.Web.Controllers
{
    public class DriverController : Controller
    {

        WebServices webServices = new WebServices();
        List<DriverViewModel> driverViewModels = new List<DriverViewModel>();

        public ActionResult Index()
        {
            PagingParameterModel pagingParameterModel = new PagingParameterModel();

            pagingParameterModel.pageNumber = 1;
            pagingParameterModel._pageSize = 1;
            pagingParameterModel.CompanyId = 1055;
            pagingParameterModel.pageSize = 100;

            var DriverList = webServices.Post(pagingParameterModel, "Driver/All");

            if(DriverList.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                driverViewModels = (new JavaScriptSerializer().Deserialize<List<DriverViewModel>>(DriverList.Data.ToString()));
            }

            return View(driverViewModels);    
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

        public ActionResult Index2()
        {
            return View();
        }
    }
}

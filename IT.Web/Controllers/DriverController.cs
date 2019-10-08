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
        DriverViewModel driverViewModel = new DriverViewModel();

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
            return View(new DriverViewModel());
        }

        [HttpPost]
        public ActionResult Create(DriverViewModel driverViewModel)
        {
            return View();
        }

       
        public ActionResult Details(int Id)
        {
            driverViewModel.Id = Id;
            driverViewModel.CompanyId = 1050;

            var result = webServices.Post(driverViewModel, "Driver/Edit");

            if(result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                if (result.Data != "[]")
                {
                    driverViewModel = (new JavaScriptSerializer().Deserialize<DriverViewModel>(result.Data.ToString()));
                }
            }

            return View(driverViewModel);
        }


        public ActionResult Edit(int Id)
        {

            driverViewModel.Id = Id;
            driverViewModel.CompanyId = 1050;

            var result = webServices.Post(driverViewModel, "Driver/Edit");

            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                if (result.Data != "[]")
                {
                    driverViewModel = (new JavaScriptSerializer().Deserialize<DriverViewModel>(result.Data.ToString()));
                }
            }

            return View("Create", driverViewModel);
        }

    }
}

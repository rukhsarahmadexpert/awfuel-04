using IT.Core.ViewModels;
using IT.Repository.WebServices;
using IT.Web.MISC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IT.Web.Controllers
{
    [Autintication]
    public class DriverController : Controller
    {

        WebServices webServices = new WebServices();
        List<DriverViewModel> driverViewModels = new List<DriverViewModel>();
        DriverViewModel driverViewModel = new DriverViewModel();
        int CompanyId = 0;

        public ActionResult Index()
        {

            CompanyId = Convert.ToInt32(Session["CompanyId"]);

            try
            {
                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.CompanyId = 1055;
                pagingParameterModel.pageSize = 100;
                pagingParameterModel.CompanyId = CompanyId;

                var DriverList = webServices.Post(pagingParameterModel, "Driver/All");

                if (DriverList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    driverViewModels = (new JavaScriptSerializer().Deserialize<List<DriverViewModel>>(DriverList.Data.ToString()));
                }

                return View(driverViewModels);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Create()
        {
            return View(new DriverViewModel());
        }

        [HttpPost]
        public ActionResult Create(DriverViewModel driverViewModel)
        {
            try
            {
                return View();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

       
        public ActionResult Details(int Id)
        {
            CompanyId = Convert.ToInt32(Session["CompanyId"]);
            try
            {
                driverViewModel.Id = Id;
                driverViewModel.CompanyId = CompanyId;

                var result = webServices.Post(driverViewModel, "Driver/Edit");

                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    if (result.Data != "[]")
                    {
                        driverViewModel = (new JavaScriptSerializer().Deserialize<DriverViewModel>(result.Data.ToString()));
                    }
                }

                return View(driverViewModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult Edit(int Id)
        {
            CompanyId = Convert.ToInt32(Session["CompanyId"]);

            try
            {

                driverViewModel.Id = Id;
                driverViewModel.CompanyId = CompanyId;

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
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

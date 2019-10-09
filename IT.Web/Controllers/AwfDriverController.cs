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
    public class AwfDriverController : Controller
    {
        WebServices webServices = new WebServices();
        List<DriverViewModel> driverViewModels = new List<DriverViewModel>();
        DriverViewModel driverViewModel = new DriverViewModel();

        public ActionResult Index()
        {
            PagingParameterModel pagingParameterModel = new PagingParameterModel();

            pagingParameterModel.pageNumber = 1;
            pagingParameterModel._pageSize = 1;
            pagingParameterModel.CompanyId = 2;
            pagingParameterModel.pageSize = 100;

            var DriverList = webServices.Post(pagingParameterModel, "AWFDriver/All");

            if (DriverList.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                driverViewModels = (new JavaScriptSerializer().Deserialize<List<DriverViewModel>>(DriverList.Data.ToString()));
            }

            return View(driverViewModels);
        }

        public ActionResult Details(int id)
        {
            try
            {

                driverViewModel.CompanyId = 2;
                driverViewModel.Id = id;

                var result = webServices.Post(driverViewModel, "AWFDriver/Edit");
                if (result.Data != null)
                {
                    driverViewModel = (new JavaScriptSerializer()).Deserialize<List<DriverViewModel>>(result.Data.ToString()).FirstOrDefault();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return View(driverViewModel);
        }
    }
    
}
        
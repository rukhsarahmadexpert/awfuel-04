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
    public class AwfDriverController : Controller
    {
        WebServices webServices = new WebServices();
        List<DriverViewModel> driverViewModels = new List<DriverViewModel>();
        List<DriverLoginHistoryViewModel> driverLoginHistoryViewModels = new List<DriverLoginHistoryViewModel>();
        DriverViewModel driverViewModel = new DriverViewModel();
        int CompanyId;

        public ActionResult Index()
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);

                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.CompanyId = CompanyId;
                pagingParameterModel.pageSize = 100;

                var DriverList = webServices.Post(pagingParameterModel, "AWFDriver/All");

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

        public ActionResult Details(int id)
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);
                driverViewModel.CompanyId = CompanyId;
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

        public ActionResult Create()
        {
            return View(new DriverViewModel());
        }

        public ActionResult DriverLoginHistoryWithAsignVehicle()
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


        public ActionResult DriverLoginHistoryAllForAdmin()
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);
                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.Id = CompanyId;
                pagingParameterModel.pageSize = 100;

                var DriverLoginList = webServices.Post(pagingParameterModel, "AWFDriver/DriverLoginHistoryAllForAdmin");
                if (DriverLoginList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {

                    driverLoginHistoryViewModels = (new JavaScriptSerializer().Deserialize<List<DriverLoginHistoryViewModel>>(DriverLoginList.Data.ToString()));
                }

                return View(driverLoginHistoryViewModels);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ReleaseVehicle(SearchViewModel searchViewModel)
        {
            try
            {
                var DriverInfo = webServices.Post(searchViewModel, "AWFDriver/ReleaseVehicle");
                if (DriverInfo.StatusCode == System.Net.HttpStatusCode.Accepted)
                {

                    driverViewModel = (new JavaScriptSerializer().Deserialize<DriverViewModel>(DriverInfo.Data.ToString()));
                }

                return Redirect(nameof(DriverLoginHistoryAllForAdmin));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        public ActionResult DriverLogouByAdmin(SearchViewModel searchViewModel)
        {
            try
            {
                var DriverInfo = webServices.Post(searchViewModel, "AWFDriver/DriverLogouByAdmin");
                if (DriverInfo.StatusCode == System.Net.HttpStatusCode.Accepted)
                {

                   var Id = (new JavaScriptSerializer().Deserialize<int>(DriverInfo.Data.ToString()));
                }

                return Redirect(nameof(DriverLoginHistoryAllForAdmin));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
    
}
        
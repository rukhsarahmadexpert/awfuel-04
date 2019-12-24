using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IT.Core.ViewModels;
using IT.Repository.WebServices;
using IT.Web.MISC;
using System.Web.Script.Serialization;

namespace IT.Web.Controllers
{
        
    public class SiteController : Controller
    {
        WebServices webServices = new WebServices();
        List<DriverViewModel> driverViewModels = new List<DriverViewModel>();
        List<SiteViewModel> siteViewModels = new List<SiteViewModel>();
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

                var SiteList = webServices.Post(pagingParameterModel, "Site/All");

                if(SiteList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    siteViewModels = (new JavaScriptSerializer().Deserialize<List<SiteViewModel>>(SiteList.Data.ToString()));
                }
                return View(siteViewModels);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



}

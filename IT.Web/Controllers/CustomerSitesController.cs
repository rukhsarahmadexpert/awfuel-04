using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using IT.Core.ViewModels;
using IT.Repository.WebServices;

namespace IT.Web.Controllers
{
    public class CustomerSitesController : Controller
    {
        WebServices webServices = new WebServices();
        List<SiteViewModel> siteViewModels = new List<SiteViewModel>();
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

                var SiteList = webServices.Post(pagingParameterModel, "CustomerSites/SiteAllCustomer");

                if (SiteList.StatusCode == System.Net.HttpStatusCode.Accepted)
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using IT.Core.ViewModels;
using IT.Repository.WebServices;
using IT.Web.MISC;

namespace IT.Web.Controllers
{
    [Autintication]
    public class QuotationController : Controller
    {
        WebServices webServices = new WebServices();
        List<DriverViewModel> driverViewModels = new List<DriverViewModel>();
        List<LPOInvoiceViewModel> lPOInvoiceViewModels = new List<LPOInvoiceViewModel>();
        List<LPOInvoiceDetails> LPOInvoiceDetails = new List<LPOInvoiceDetails>();
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

                var SiteList = webServices.Post(new LPOInvoiceViewModel(), "Quotation/All");

                if (SiteList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    lPOInvoiceViewModels = (new JavaScriptSerializer().Deserialize<List<LPOInvoiceViewModel>>(SiteList.Data.ToString()));
                }
                return View(lPOInvoiceViewModels);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}

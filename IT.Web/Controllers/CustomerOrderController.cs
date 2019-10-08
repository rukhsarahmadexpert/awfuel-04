using IT.Core.ViewModels;
using IT.Repository.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IT.Web.Controllers
{
    public class CustomerOrderController : Controller
    {

        WebServices webServices = new WebServices();
        List<CustomerOrderViewModel>  customerOrderViewModels = new List<CustomerOrderViewModel>();
        CustomerOrderViewModel CustomerOrderViewModel = new CustomerOrderViewModel();


        public ActionResult Index()
        {
            PagingParameterModel pagingParameterModel = new PagingParameterModel();

            pagingParameterModel.pageNumber = 1;
            pagingParameterModel._pageSize = 1;
            pagingParameterModel.CompanyId = 1055;
            pagingParameterModel.pageSize = 100;

            var CustomerOrderList = webServices.Post(pagingParameterModel, "CustomerOrder/All");

            if (CustomerOrderList.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                customerOrderViewModels = (new JavaScriptSerializer().Deserialize<List<CustomerOrderViewModel>>(CustomerOrderList.Data.ToString()));
            }

            return View(customerOrderViewModels);
        }
    }
}

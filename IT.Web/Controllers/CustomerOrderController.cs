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
       CustomerOrderGroupViewModel customerOrderGroupViewModel = new CustomerOrderGroupViewModel();



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


        public ActionResult Details(int Id)
        {

            var customerOrderGroup = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerGroupOrderById/" + Id);

            if (customerOrderGroup.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                customerOrderGroupViewModel = (new JavaScriptSerializer().Deserialize<CustomerOrderGroupViewModel>(customerOrderGroup.Data.ToString()));
            }


            var CustomerOrderGroupDetailsList = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerGroupOrderDetailsByOrderId/" + Id);

            if(CustomerOrderGroupDetailsList.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                customerOrderGroupViewModel.customerGroupOrderDetailsViewModels = (new JavaScriptSerializer().Deserialize<List<CustomerGroupOrderDetailsViewModel>>(CustomerOrderGroupDetailsList.Data.ToString()));
            }

            return View(customerOrderGroupViewModel);

        }

    }

}

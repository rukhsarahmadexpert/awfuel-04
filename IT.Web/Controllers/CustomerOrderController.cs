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
        List<CustomerNoteOrderViewModel> customerNoteOrderViewModel = new List<CustomerNoteOrderViewModel>();
        CustomerOrderViewModel CustomerOrderViewModel = new CustomerOrderViewModel();
        CustomerOrderGroupViewModel customerOrderGroupViewModel = new CustomerOrderGroupViewModel();
        int CompanyId = 0;


        public ActionResult Index()
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);

                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.CompanyId = CompanyId;
                pagingParameterModel.OrderProgress = "all";
                pagingParameterModel.IsSend = false;
                pagingParameterModel.pageSize = 100;


                var CustomerOrderList = webServices.Post(pagingParameterModel, "CustomerOrder/CustomerOrderAllByCompanyId");

                if (CustomerOrderList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerNoteOrderViewModel = (new JavaScriptSerializer().Deserialize<List<CustomerNoteOrderViewModel>>(CustomerOrderList.Data.ToString()));
                }

                return View(customerNoteOrderViewModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public JsonResult GetAll(string OrderProgress)
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);
                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.CompanyId = CompanyId;
                pagingParameterModel.OrderProgress = OrderProgress;
                pagingParameterModel.IsSend = true;
                pagingParameterModel.pageSize = 100;


                var CustomerOrderList = webServices.Post(pagingParameterModel, "CustomerOrder/CustomerOrderAllByCompanyId");

                if (CustomerOrderList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerNoteOrderViewModel = (new JavaScriptSerializer().Deserialize<List<CustomerNoteOrderViewModel>>(CustomerOrderList.Data.ToString()));
                }

                return Json(customerNoteOrderViewModel,JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public ActionResult Details(int Id)
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);

                var customerOrderGroup = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerOrderAllByCompanyId/ " + Id);

                if (customerOrderGroup.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerOrderGroupViewModel = (new JavaScriptSerializer().Deserialize<CustomerOrderGroupViewModel>(customerOrderGroup.Data.ToString()));
                }


                var CustomerOrderGroupDetailsList = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerGroupOrderDetailsByOrderId/" + Id);

                if (CustomerOrderGroupDetailsList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerOrderGroupViewModel.customerGroupOrderDetailsViewModels = (new JavaScriptSerializer().Deserialize<List<CustomerGroupOrderDetailsViewModel>>(CustomerOrderGroupDetailsList.Data.ToString()));
                }

                return View(customerOrderGroupViewModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }



        public ActionResult Admin()
        {
            try
            {
               
                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.CompanyId = CompanyId;
                pagingParameterModel.OrderProgress = "All";
                pagingParameterModel.IsSend = true;
                pagingParameterModel.pageSize = 10;


                var CustomerOrderList = webServices.Post(pagingParameterModel, "CustomerOrder/GetAllCustomerOrderGroupByAdmin");

                if (CustomerOrderList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerNoteOrderViewModel = (new JavaScriptSerializer().Deserialize<List<CustomerNoteOrderViewModel>>(CustomerOrderList.Data.ToString()));
                }

                return View(customerNoteOrderViewModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult AdminDetails(int Id)
        {
            try
            {
                var customerOrderGroup = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/GetAllCustomerOrderGroupByAdmin/ " + Id);

                if (customerOrderGroup.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerOrderGroupViewModel = (new JavaScriptSerializer().Deserialize<CustomerOrderGroupViewModel>(customerOrderGroup.Data.ToString()));
                }


                var CustomerOrderGroupDetailsList = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerGroupOrderDetailsByOrderId/" + Id);

                if (CustomerOrderGroupDetailsList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerOrderGroupViewModel.customerGroupOrderDetailsViewModels = (new JavaScriptSerializer().Deserialize<List<CustomerGroupOrderDetailsViewModel>>(CustomerOrderGroupDetailsList.Data.ToString()));
                }

                return View(customerOrderGroupViewModel);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}

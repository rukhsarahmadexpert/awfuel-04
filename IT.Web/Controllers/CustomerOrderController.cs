using IT.Core.ViewModels;
using IT.Repository.WebServices;
using IT.Web.MISC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IT.Web.Controllers
{
    [Autintication]
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


                var CustomerOrderList = webServices.Post(pagingParameterModel, "CustomerOrder/CustomerOrderAllByCompanyId",false);

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
                //CompanyId = Convert.ToInt32(Session["CompanyId"]);
                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.OrderProgress = OrderProgress;
                //if (OrderProgress != "All")
                //{
                //    pagingParameterModel.CompanyId = CompanyId;
                //}                
                pagingParameterModel.IsSend = true;
                pagingParameterModel.pageSize = 100;


                var CustomerOrderList = webServices.Post(pagingParameterModel, "CustomerOrder/CustomerOrderAllByCompanyId",false);

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

                var customerOrderGroup = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerOrderAllByCompanyId/ " + Id,false);

                if (customerOrderGroup.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerOrderGroupViewModel = (new JavaScriptSerializer().Deserialize<CustomerOrderGroupViewModel>(customerOrderGroup.Data.ToString()));
                }


                var CustomerOrderGroupDetailsList = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerGroupOrderDetailsByOrderId/" + Id,false);

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


        [AcceptVerbs(HttpVerbs.Post | HttpVerbs.Get)]
        public ActionResult Admin(string OrderProgress)
        {
            List<CustomerNoteOrderViewModel> customerNoteOrderViewModels = new List<CustomerNoteOrderViewModel>();
            try
            {
                if(OrderProgress == null)
                {
                    OrderProgress = "All";
                }
               
                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.CompanyId = CompanyId;
                pagingParameterModel.OrderProgress = OrderProgress;
                pagingParameterModel.IsSend = true;
                pagingParameterModel.pageSize = 10;


                var CustomerOrderList = webServices.Post(pagingParameterModel, "CustomerOrder/GetAllCustomerOrderGroupByAdmin",false);

                if (CustomerOrderList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    if (CustomerOrderList.Data != null && CustomerOrderList.Data != "[]")
                    {
                        customerNoteOrderViewModels = (new JavaScriptSerializer().Deserialize<List<CustomerNoteOrderViewModel>>(CustomerOrderList.Data.ToString()));
                    }
                }

                return View(customerNoteOrderViewModels);
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
                var customerOrderGroup = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/GetAllCustomerOrderGroupByAdmin/ " + Id,false);

                if (customerOrderGroup.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerOrderGroupViewModel = (new JavaScriptSerializer().Deserialize<CustomerOrderGroupViewModel>(customerOrderGroup.Data.ToString()));
                }


                var CustomerOrderGroupDetailsList = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerGroupOrderDetailsByOrderId/" + Id,false);

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


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult OrderDetails(int Id)
        {
            try
            {
                CustomerOrderGroupViewModel customerOrderGroupViewModel = new CustomerOrderGroupViewModel();

                var customerOrderGroup = webServices.Post(new CustomerOrderGroupViewModel(), "CustomerOrder/CustomerGroupOrderById/ " + Id, false);

                if (customerOrderGroup.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerOrderGroupViewModel = (new JavaScriptSerializer().Deserialize<CustomerOrderGroupViewModel>(customerOrderGroup.Data.ToString()));
                }

                return View(customerOrderGroupViewModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AcceptOrder(CustomerOrderViewModel customerOrderViewModel)
        {
            try
            {
                
                var Result = webServices.Post(customerOrderViewModel, "CustomerOrder/CustomerOrderRejectAcceptByAdmin", false);

                if(Result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    if(Result.Data != "[]")
                    {
                        int Res = (new JavaScriptSerializer().Deserialize<int>(Result.Data));
                    }
                }

                return Json("suceess",JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Failed", JsonRequestBehavior.AllowGet);
            }
        }

    }
}

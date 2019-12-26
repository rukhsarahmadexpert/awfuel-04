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
    public class FuelTransferController : Controller
    {
        WebServices webServices = new WebServices();
        List<FuelTransferViewModel> fuelTransferViewModels = new List<FuelTransferViewModel>();
        List<OrderTransferRequestsViewModel> orderTransferRequestsViewModels = new List<OrderTransferRequestsViewModel>();
        List<SearchViewModel> searchViewModels = new List<SearchViewModel>();
        int CompanyId;
        public ActionResult Index()
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["companyId"]);

                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.CompanyId = CompanyId;
                pagingParameterModel.pageSize = 100;

                var FuelTransferList = webServices.Post(new FuelTransferViewModel(), "FuelTransfer/All");

                if(FuelTransferList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    fuelTransferViewModels = (new JavaScriptSerializer().Deserialize<List<FuelTransferViewModel>>(FuelTransferList.Data.ToString()));
                }

                return View(fuelTransferViewModels);

            }
            catch (Exception ex)
            {
                throw ex;

            }
            
        }

        public ActionResult OrderTransferRequestsAll()
        {
            orderTransferRequestsViewModels = new List<OrderTransferRequestsViewModel>();

            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);
                SearchViewModel searchViewModel = new SearchViewModel();
                searchViewModel.CompanyId = CompanyId;
                var OrderTransferRquestViewList = webServices.Post(searchViewModel, "FuelTransfer/OrderTransferRequestsAll");
                if (OrderTransferRquestViewList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    orderTransferRequestsViewModels = (new JavaScriptSerializer().Deserialize<List<OrderTransferRequestsViewModel>>(OrderTransferRquestViewList.Data.ToString()));
                }
                return View(orderTransferRequestsViewModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult OrderTransferRequestsAllByDriverId(OrderTransferRequestsViewModel orderTransferRequestsViewModel)
        {
            orderTransferRequestsViewModels = new List<OrderTransferRequestsViewModel>();
            try
            { 
                PagingParameterModel pagingParameterModel = new PagingParameterModel();
                pagingParameterModel.DriverId = orderTransferRequestsViewModel.DriverId;
                var OrderTransferRquestViewList = webServices.Post(pagingParameterModel, "FuelTransfer/OrderTransferRequestsAllByDriverId");
                if (OrderTransferRquestViewList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    orderTransferRequestsViewModels = (new JavaScriptSerializer().Deserialize<List<OrderTransferRequestsViewModel>>(OrderTransferRquestViewList.Data.ToString()));
                }
                return View("OrderTransferRequestsAll", orderTransferRequestsViewModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult OrderTransferRequestsAdminAcceptOrReject(OrderTransferRequestsViewModel orderTransferRequestsViewModel)
        { 
            try
            {
                orderTransferRequestsViewModel.AcceptBy = Convert.ToInt32(Session["UserId"]);
                var OrderTransferRquestViewList = webServices.Post(orderTransferRequestsViewModel, "FuelTransfer/OrderTransferRequestsAdminAcceptOrReject");
                if (OrderTransferRquestViewList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    orderTransferRequestsViewModel = (new JavaScriptSerializer().Deserialize<OrderTransferRequestsViewModel>(OrderTransferRquestViewList.Data.ToString()));
                }
                if (orderTransferRequestsViewModel.IsAccepted == false)
                {
                    return Redirect(nameof(OrderTransferRequestsAll));
                }
                else
                {

                    return Json(orderTransferRequestsViewModel, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}

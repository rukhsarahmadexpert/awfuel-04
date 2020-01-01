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
    public class DirectSaleController : Controller
    {
        WebServices webServices = new WebServices();
        List<DriverModel> driverModels = new List<DriverModel>();
        List<ProductViewModel> productViewModels = new List<ProductViewModel>();
        List<DirectSaleViewModel> directSaleViewModels = new List<DirectSaleViewModel>();
        List<VehicleViewModel> vehicleViewModels = new List<VehicleViewModel>();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DirectSaleCreate()
        {
            return View();
        }

        [HttpPost]
        public JsonResult VehicleByTrafficPlateNumber(DriverModel driverModel)
        {
            try
            {

                var DriverViewList = webServices.Post(driverModel, "Vehicle/VehicleByTrafficPlateNumber");
                if (DriverViewList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    driverModels = (new JavaScriptSerializer().Deserialize<List<DriverModel>>(DriverViewList.Data.ToString()));

                    return Json(driverModels, JsonRequestBehavior.AllowGet);
                }

                return Json("failed", JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);
            }
        }
        
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(DriverModel driverModel)
        {
            try
            {
                  driverModel.createdBy = Convert.ToInt32(Session["UserId"]);

                var DriverViewModelList = webServices.Post(driverModel, "Vehicle/DirectSaleVehicleAndDriverAdd");
                if (DriverViewModelList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    driverModel = (new JavaScriptSerializer().Deserialize<DriverModel>(DriverViewModelList.Data.ToString()));



                    TempData["driverModel"] = driverModel;

                    //return View("DirectsaleOrderAdd", driverModel);
                    return RedirectToAction(nameof(DirectsaleOrderAdd));

                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult DirectsaleOrderAdd()
        {
            DriverModel driverModel = new DriverModel();
            driverModel = TempData["driverModel"] as DriverModel;

            //if (driverModel.TraficPlateNumber == null)
            //{
            //    return RedirectToAction(nameof(DirectSaleCreate));
            //}
            //else
            //{

            var productList = webServices.Post(new ProductViewModel(), "Product/All");

            if (productList.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                productViewModels = (new JavaScriptSerializer().Deserialize<List<ProductViewModel>>(productList.Data.ToString()));
            }
            ViewBag.productViewModels = productViewModels;

            TempData.Keep();

            return View(driverModel);
            //}

        }

        [HttpPost]
        public ActionResult DirectsaleOrderAdd(CustomerOrderListViewModel customerOrderListViewModel)
        {

            try
            {

                customerOrderListViewModel.CreatedBy = Convert.ToInt32(Session["UserId"]);
                customerOrderListViewModel.CreatedDate = System.DateTime.Now;

                var CustomerOrderList = webServices.Post(customerOrderListViewModel, "CustomerOrder/CustomerOrderGroupDirectSaleAdd");
                if (CustomerOrderList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    customerOrderListViewModel = (new JavaScriptSerializer().Deserialize<CustomerOrderListViewModel>(CustomerOrderList.Data.ToString()));

                    //return View("DirectsaleOrderAdd", driverModel);
                    //return RedirectToAction(nameof(DirectSaleCreate));
                    return Json("success", JsonRequestBehavior.AllowGet);

                }
                else
                {
                    DriverModel driverModel = new DriverModel();
                    driverModel = TempData["driverModel"] as DriverModel;
                    var productList = webServices.Post(new ProductViewModel(), "Product/All");
                    if (productList.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        productViewModels = (new JavaScriptSerializer().Deserialize<List<ProductViewModel>>(productList.Data.ToString()));
                    }
                    ViewBag.productViewModels = productViewModels;
                    TempData.Keep();
                    return View(driverModel);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public ActionResult AllCashCompanyVehicle()
        {
            try
            {

                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                var DriverViewList = webServices.Post(pagingParameterModel, "Vehicle/AllCashCompanyVehicle");
                if (DriverViewList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    vehicleViewModels = (new JavaScriptSerializer().Deserialize<List<VehicleViewModel>>(DriverViewList.Data.ToString()));
                }
                return View(vehicleViewModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
        public ActionResult Details(int Id)
        {
            try
            {
                DriverModel driverModel = new DriverModel();
                driverModel.VehicleId = Id;
                var DetailsList = webServices.Post(driverModel, "Vehicle/DirectSaleDetailsByVehicleId");
                if (DetailsList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    directSaleViewModels = (new JavaScriptSerializer().Deserialize<List<DirectSaleViewModel>>(DetailsList.Data.ToString()));
                    return View(directSaleViewModels);
                }

                return View(directSaleViewModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }


        public ActionResult GetLocation()
        {
            return View();
        }

    }
}

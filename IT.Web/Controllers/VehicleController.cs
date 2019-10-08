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
    public class VehicleController : Controller
    {

        WebServices webServices = new WebServices();
        List<VehicleViewModel> vehicleViewModels = new List<VehicleViewModel>();
        VehicleViewModel vehicleViewModel = new VehicleViewModel();
        List<VehicleTypeViewModel> vehicleTypeViewModels = new List<VehicleTypeViewModel>();

        public List<DriverViewModel> VehicleViewModel { get; private set; }
        public List<VehicleViewModel> VehicleViewModels { get; private set; }

        // GET: Vehicle
        public ActionResult Index()
        {
            PagingParameterModel pagingParameterModel = new PagingParameterModel();

            pagingParameterModel.pageNumber = 1;
            pagingParameterModel._pageSize = 1;
            pagingParameterModel.CompanyId = 1055;
            pagingParameterModel.pageSize = 100;

            var VehicleList = webServices.Post(pagingParameterModel, "Vehicle/All");

            if (VehicleList.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                VehicleViewModels = (new JavaScriptSerializer().Deserialize<List<VehicleViewModel>>(VehicleList.Data.ToString()));
            }

            return View(VehicleViewModels);
            
        }


        public JsonResult GetAllVehicle()
        {
            try
            {
            int CompanyId = 1;
            var result = webServices.Post(new VehicleViewModel(), "Vehicle/All/" + CompanyId);
            if (result.Data != null)
            {
                vehicleViewModels = (new JavaScriptSerializer()).Deserialize<List<VehicleViewModel>>(result.Data.ToString());
            }

            return Json(vehicleViewModels, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // GET: Vehicle/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var result = webServices.Post(new VehicleViewModel(), "Vehicle/Edit/" + id);
                if (result.Data != null)
                {
                    vehicleViewModel = (new JavaScriptSerializer()).Deserialize<List<VehicleViewModel>>(result.Data.ToString()).FirstOrDefault();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return View(vehicleViewModel);
        }


        // GET: Vehicle/Create
        public ActionResult Create()
        {
            try
            {
                var result = webServices.Post(new VehicleViewModel(), "VehicleType/GetAll");
                if (result.Data != null)
                {
                    vehicleTypeViewModels = (new JavaScriptSerializer()).Deserialize<List<VehicleTypeViewModel>>(result.Data.ToString());
                }

                ViewBag.vehicleTypeViewModels = vehicleTypeViewModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return View();
        }

        // POST: Vehicle/Create
        [HttpPost]
        public ActionResult Create(VehicleViewModel vehicleViewModel)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicle/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                var result = webServices.Post(new VehicleViewModel(), "Vehicle/Edit/" + id);
                if (result.Data != null)
                {
                    vehicleViewModel = (new JavaScriptSerializer()).Deserialize<VehicleViewModel>(result.Data.ToString());
                }

                var resultVehicleType = webServices.Post(new VehicleViewModel(), "VehicleType/GetAll");
                if (result.Data != null)
                {
                    vehicleTypeViewModels = (new JavaScriptSerializer()).Deserialize<List<VehicleTypeViewModel>>(resultVehicleType.Data.ToString());
                }

                ViewBag.vehicleTypeViewModels = vehicleTypeViewModels;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(vehicleViewModel);
        }

        // POST: Vehicle/Edit/5
        [HttpPost]
        public ActionResult Edit(VehicleViewModel vehicleViewModel)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Vehicle/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Vehicle/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

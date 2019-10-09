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
    public class EmployeeController : Controller
    {

        WebServices webServices = new WebServices();
        List<CountryViewModel> CountryViewModel = new List<CountryViewModel>();
        List<DesignationViewModel> designationViewModels = new List<DesignationViewModel>();
        List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();
        EmployeeViewModel employeeViewModel = new EmployeeViewModel();
        // GET: Employee
        public ActionResult Index()
        {
            try
            {
                int CompanyId = 2;

                if (HttpContext.Cache["EmployeeDatas"] != null)
                {

                    employeeViewModels = HttpContext.Cache["EmployeeDatas"] as List<EmployeeViewModel>;
                }
                else
                {
                    var results = webServices.Post(new EmployeeViewModel(), "AWFEmployee/All/" + CompanyId);
                    if (results.StatusCode == System.Net.HttpStatusCode.Accepted)
                    {
                        employeeViewModels = (new JavaScriptSerializer()).Deserialize<List<EmployeeViewModel>>(results.Data.ToString());

                        HttpContext.Cache["EmployeeDatas"] = employeeViewModels;
                    }
                }

                return View(employeeViewModels);
              

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // GET: Employee/Details/5
        public ActionResult Details(int Id)
        {
            employeeViewModel.Id = Id;
            employeeViewModel.CompanyId = 2;

            var result = webServices.Post(new EmployeeViewModel(), "AWFEmployee/Edit/"+Id);

            if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                if (result.Data != "[]")
                {
                    employeeViewModel = (new JavaScriptSerializer().Deserialize<EmployeeViewModel>(result.Data.ToString()));
                }
            }

            return View(employeeViewModel);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
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

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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

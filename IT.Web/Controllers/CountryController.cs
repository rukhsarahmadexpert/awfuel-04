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

    public class CountryController : Controller
    {
        WebServices webServices = new WebServices();
        CountryViewModel countryViewModel = new CountryViewModel();
        List<CountryViewModel> countryViewModels = new List<CountryViewModel>();
        // GET: Country
        public ActionResult Index()
        {
            try
            {
                var countryList = webServices.Post(new CountryViewModel(), "Country/GetAll");

                if (countryList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    countryViewModels = (new JavaScriptSerializer().Deserialize<List<CountryViewModel>>(countryList.Data.ToString()));
                }
                if (Request.IsAjaxRequest())
                {
                    return Json(countryViewModels, JsonRequestBehavior.AllowGet);
                }
                return View(countryViewModels);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ActionResult Create()
        {
            return View(new CountryViewModel());
        }

        [HttpPost]
        public ActionResult Create(CountryViewModel countryViewModel)
        {
            try
            {

                countryViewModels.Insert(0, countryViewModel);
                var CountryResult = new ServiceResponseModel();
                if (countryViewModel.Id < 1)
                {

                    CountryResult = webServices.Post(countryViewModels, "Country/Add");
                }
                else
                {

                    CountryResult = webServices.Post(countryViewModel, "Country/Update");
                }

                if (CountryResult.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    var reuslt = (new JavaScriptSerializer().Deserialize<int>(CountryResult.Data));

                    return RedirectToAction(nameof(Index));
                }

                return View(CountryResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public ActionResult Edit(int Id, String Name)
        {
            CountryViewModel countryViewModel = new CountryViewModel();
            countryViewModel.Id = Id;
            countryViewModel.CountryName = Name;
            return View("Create",countryViewModel);
        }


        [NonAction]
        public List<CountryViewModel> Countries()
        {
            try
            {
                var countryList = webServices.Post(new ProductViewModel(), "Country/All");

                if (countryList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    countryViewModels = (new JavaScriptSerializer().Deserialize<List<CountryViewModel>>(countryList.Data.ToString()));
                 
                }
                return countryViewModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
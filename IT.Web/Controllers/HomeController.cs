using IT.Core.ViewModels;
using IT.Repository.WebServices;
using IT.Web.MISC;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace IT.Web.Controllers
{


    [Autintication]
    public class HomeController : Controller
    {

        List<CustomerNotificationViewModel> customerNotificationViewModels = new List<CustomerNotificationViewModel>();
        WebServices webServices = new WebServices();

        public ActionResult Index()

        {
            
            if (HttpContext.Cache["customerNotificationViewModels"] == null)
            {
               var result = webServices.Post(new CustomerNotificationViewModel(), "Advertisement/All");

                if(result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    if(result.Data != null)
                    {
                        customerNotificationViewModels = (new JavaScriptSerializer().Deserialize<List<CustomerNotificationViewModel>>(result.Data.ToString()));
                        HttpContext.Cache["customerNotificationViewModels"] = customerNotificationViewModels;
                    }
                }

            }
            else
            {
                customerNotificationViewModels = HttpContext.Cache["customerNotificationViewModels"] as List<CustomerNotificationViewModel>;
            }

            ViewBag.customerNotificationViewModels = customerNotificationViewModels;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CustomerHomes()
        {
            return View();
        }


        public ActionResult AdminHome()
        {
            return View();
        }

        public ActionResult Policy()
        {
            return View();
        }

        public ActionResult StorgeGraphPartialView()
        {
            return PartialView("~/Views/Shared/PartialView/StorageGraph/StorgeGraphPartialView.cshtml");
        }
    }
}
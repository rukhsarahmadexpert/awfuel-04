using IT.Web.MISC;
using System.Web.Mvc;

namespace IT.Web.Controllers
{
    [Autintication]
    public class HomeController : Controller
    {
         
        public ActionResult Index()
        {
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
    }
}
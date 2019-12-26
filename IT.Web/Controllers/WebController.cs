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
    public class WebController : Controller
    {
        WebServices webServices = new WebServices();
        List<AppVersionViewModel> appVersionViewModels = new List<AppVersionViewModel>();
     
        public ActionResult Index(int Id = 0)
        {
           var AppVersionList = webServices.Post(new AppVersionViewModel(), "AppVersion/AppVersionAll");

            if (AppVersionList.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                appVersionViewModels = (new JavaScriptSerializer().Deserialize<List<AppVersionViewModel>>(AppVersionList.Data.ToString()));
            }
            if (Id > 0)
            {
                var Model = appVersionViewModels.Where(x => x.Id == Id).FirstOrDefault();
                return View("AppVersionAdd", Model);
            }
            else
            {
                return View(appVersionViewModels);
            }
           

        }

        public ActionResult AppVersionAdd()
        {
           
            return View(new AppVersionViewModel());

        }

        [HttpPost]
        public ActionResult PostAppVersion(AppVersionViewModel appVersionViewModel)
        {

            try
            {

                var result = webServices.Post(appVersionViewModel, "AppVersion/AppVersionAdd");

                if(result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    return Redirect(nameof(Index));
                }
                else
                {
                    return View(appVersionViewModel);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

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
    public class UserController : Controller
    {
        UserViewModel userViewModel = new UserViewModel();
        List<UserViewModel> userViewModelList = new List<UserViewModel>();        
        WebServices webServices = new WebServices();
        public ActionResult Index()
        {
            //var result = webServices.Post(new UserViewModel(), "User/GetAll");
          //  userViewModelList = (new JavaScriptSerializer()).Deserialize<List<UserViewModel>>(result.Data.ToString());
            return View();
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(UserViewModel userViewModel)
        {
           
            //var resultData = (new JavaScriptSerializer()).Deserialize<object>(result.Data.ToString());
            //return View();
            if (Convert.ToInt32(result.Data) > 0)
                return RedirectToAction("Index");
            else
                return View(userViewModel);
        }


        public ActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            // loginViewModel.UserName = "j12@gmail.com";
            //  loginViewModel.Password = "123456";

            try
            {
                var result = webServices.Post(userViewModel, "User/Add");

                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    var UserDate = (new JavaScriptSerializer()).Deserialize<object>(result.Data.ToString());

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(loginViewModel);
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult Registration(UserViewModel userViewModel)
        {
            return View();
        }


    }
}
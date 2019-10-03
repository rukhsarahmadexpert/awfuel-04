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
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            LoginViewModel loginViewModel = new LoginViewModel();

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                var result = webServices.Post(loginViewModel, "User/Login");

                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    var UserDate = (new JavaScriptSerializer()).Deserialize<object>(result.Data.ToString());

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(UserViewModel userViewModel)
        {
            UserCompanyViewModel userCompanyViewModel = new UserCompanyViewModel();

            try
            {
                var result = webServices.Post(userViewModel, "User/Register");

                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    userCompanyViewModel = (new JavaScriptSerializer()).Deserialize<UserCompanyViewModel>(result.Data.ToString());


                    if (userCompanyViewModel.CompanyId > 0)
                    {

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return RedirectToAction("Create", "Company");
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return View();
        }
    }
}
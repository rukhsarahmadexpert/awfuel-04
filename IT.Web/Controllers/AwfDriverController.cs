using IT.Core.ViewModels;
using IT.Repository.WebServices;
using IT.Web.MISC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


namespace IT.Web.Controllers
{
    [Autintication]
    public class AwfDriverController : Controller
    {
        WebServices webServices = new WebServices();
        List<DriverViewModel> driverViewModels = new List<DriverViewModel>();
        List<DriverLoginHistoryViewModel> driverLoginHistoryViewModels = new List<DriverLoginHistoryViewModel>();
        DriverViewModel driverViewModel = new DriverViewModel();
        int CompanyId;

        public ActionResult Index()
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);

                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.CompanyId = CompanyId;
                pagingParameterModel.pageSize = 100;

                var DriverList = webServices.Post(pagingParameterModel, "AWFDriver/All");

                if (DriverList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    driverViewModels = (new JavaScriptSerializer().Deserialize<List<DriverViewModel>>(DriverList.Data.ToString()));
                }

                return View(driverViewModels);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);
                driverViewModel.CompanyId = CompanyId;
                driverViewModel.Id = id;

                var result = webServices.Post(driverViewModel, "AWFDriver/Edit");
                if (result.Data != null)
                {
                    driverViewModel = (new JavaScriptSerializer()).Deserialize<List<DriverViewModel>>(result.Data.ToString()).FirstOrDefault();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return View(driverViewModel);
        }

        public ActionResult Create()
        {
            return View(new DriverViewModel());
        }


        [HttpPost]
        public ActionResult Create(DriverViewModel driverViewModel)
        {
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase[] httpPostedFileBase = new HttpPostedFileBase[8];
                     
                    httpPostedFileBase[0] = driverViewModel.PassportBackFile;
                    httpPostedFileBase[1] = driverViewModel.DriverImageUrlFile;
                    httpPostedFileBase[2] = driverViewModel.DrivingLicenseBackFile;
                    httpPostedFileBase[3] = driverViewModel.DrivingLicenseFrontFile;
                    httpPostedFileBase[4] = driverViewModel.IDUAECopyBackFile;
                    httpPostedFileBase[5] = driverViewModel.IDUAECopyFrontFile;
                    httpPostedFileBase[6] = driverViewModel.VisaCopyFile;
                    httpPostedFileBase[7] = driverViewModel.PassportCopyFile;

                    var file = driverViewModel.PassportBackFile;

                    using (HttpClient client = new HttpClient())
                    {
                        using (var content = new MultipartFormDataContent())
                        {

                            if (httpPostedFileBase.ToList().Count > 0)
                            {

                                for(int i =0; i< 8; i++)
                                {
                                    file = httpPostedFileBase[i];

                                    byte[] fileBytes = new byte[file.InputStream.Length + 1];
                                    file.InputStream.Read(fileBytes, 0, fileBytes.Length);
                                    var fileContent = new ByteArrayContent(fileBytes);

                                    if (i == 0)
                                    {
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("PassportBack") { FileName = file.FileName };
                                    }
                                    else if (i == 1)
                                    {
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("DriverImageUrl") { FileName = file.FileName };
                                    }
                                    else if (i == 2)
                                    {
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("DrivingLicenseBackFil") { FileName = file.FileName };
                                    }
                                    else if (i == 3)
                                    {
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("DrivingLicenseFront") { FileName = file.FileName };
                                    }
                                    else if (i == 4)
                                    {
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("IDUAECopyBack") { FileName = file.FileName };
                                    }
                                    else if (i == 5)
                                    {
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("IDUAECopyFront") { FileName = file.FileName };
                                    }
                                    else if (i == 6)
                                    {
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("VisaCopy") { FileName = file.FileName };
                                    }
                                    else if (i == 7)
                                    {
                                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("PassportCopy") { FileName = file.FileName };
                                    }
                                    content.Add(fileContent);
                                }
                            }
                            string UserId = Session["UserId"].ToString();
                         
                              
                              
                                var result = webServices.PostMultiPart(content, "", true);
                                if (result.StatusCode == System.Net.HttpStatusCode.Accepted)
                                {
                                    return Redirect(nameof(Index));
                                }
                        
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DriverLoginHistoryWithAsignVehicle()
        {
            try
            {

                return View();

            }
            catch(Exception ex)
            {

                throw ex;

            }
        }


        public ActionResult DriverLoginHistoryAllForAdmin()
        {
            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);
                PagingParameterModel pagingParameterModel = new PagingParameterModel();

                pagingParameterModel.pageNumber = 1;
                pagingParameterModel._pageSize = 1;
                pagingParameterModel.Id = CompanyId;
                pagingParameterModel.pageSize = 100;

                var DriverLoginList = webServices.Post(pagingParameterModel, "AWFDriver/DriverLoginHistoryAllForAdmin");
                if (DriverLoginList.StatusCode == System.Net.HttpStatusCode.Accepted)
                {

                    driverLoginHistoryViewModels = (new JavaScriptSerializer().Deserialize<List<DriverLoginHistoryViewModel>>(DriverLoginList.Data.ToString()));
                }

                return View(driverLoginHistoryViewModels);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult ReleaseVehicle(SearchViewModel searchViewModel)
        {
            try
            {
                var DriverInfo = webServices.Post(searchViewModel, "AWFDriver/ReleaseVehicle");
                if (DriverInfo.StatusCode == System.Net.HttpStatusCode.Accepted)
                {

                    driverViewModel = (new JavaScriptSerializer().Deserialize<DriverViewModel>(DriverInfo.Data.ToString()));
                }

                return Redirect(nameof(DriverLoginHistoryAllForAdmin));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPost]
        public ActionResult DriverLogouByAdmin(SearchViewModel searchViewModel)
        {
            try
            {
                var DriverInfo = webServices.Post(searchViewModel, "AWFDriver/DriverLogouByAdmin");
                if (DriverInfo.StatusCode == System.Net.HttpStatusCode.Accepted)
                {

                   var Id = (new JavaScriptSerializer().Deserialize<int>(DriverInfo.Data.ToString()));
                }

                return Redirect(nameof(DriverLoginHistoryAllForAdmin));

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public ActionResult DriverAllOnline()
        {

            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);

                var DriverInfo = webServices.Post(new DriverViewModel(), "AWFDriver/DriverAllOnline/"+CompanyId);
                if (DriverInfo.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    var OnlineDriverList = (new JavaScriptSerializer().Deserialize<List<DriverViewModel>>(DriverInfo.Data.ToString()));
                    return Json(OnlineDriverList, JsonRequestBehavior.AllowGet);
                }

                return Json("failed", JsonRequestBehavior.AllowGet);
                

            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);

            }  

        }


        [HttpPost]
        public ActionResult DriverAllOnlineByDriverId(int Id)
        {

            try
            {
                CompanyId = Convert.ToInt32(Session["CompanyId"]);

                var DriverInfo = webServices.Post(new DriverViewModel(), "AWFDriver/DriverAllOnline/" + CompanyId);
                if (DriverInfo.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    var OnlineDriverList = (new JavaScriptSerializer().Deserialize<List<DriverViewModel>>(DriverInfo.Data.ToString()));

                    var SingleDriver = OnlineDriverList.Where(x => x.Id == Id).FirstOrDefault();
                   
                        return Json(SingleDriver, JsonRequestBehavior.AllowGet);
                    
                    
                }

                return Json("failed", JsonRequestBehavior.AllowGet);


            }
            catch (Exception)
            {
                return Json("failed", JsonRequestBehavior.AllowGet);

            }

        }

    }
    
}
        
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using IT.Core.ViewModels;


namespace IT.Web.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index()
        {
            return View();
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            try
            {
                using (var client = new HttpClient())
                using (var content = new MultipartFormDataContent())
                {
                    // Make sure to change API address
                      client.BaseAddress = new Uri("http://localhost:64299/api/");
                    //client.BaseAddress = new Uri("http://itmolen-001-site8.htempurl.com/api/");

                    // Add first file content 
                    var fileContent1 = new ByteArrayContent(System.IO.File.ReadAllBytes(@"C:\Users\IT Molen\Pictures\pic 2.PNG"));
                    fileContent1.Headers.ContentDisposition = new ContentDispositionHeaderValue("LogoUrl")
                    {
                        FileName = "Sample.pdf"
                    };

                    fileContent1.Headers.Add("Hello", "Hello");
                    fileContent1.Headers.Add("Address", "Hello");
                    fileContent1.Headers.Add("City", "Hello");
                    fileContent1.Headers.Add("Country", "Hello");
                    fileContent1.Headers.Add("Id", "2097");
                    fileContent1.Headers.Add("ClientDocs", "ClientDocs");

                    // Add Second file content
                    // var fileContent2 = new ByteArrayContent(System.IO.File.ReadAllBytes(@"c:\Users\aisadmin\Desktop\Sample.txt"));
                    // fileContent2.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    // {
                    //     FileName = "Sample.txt"
                    //  };

                    content.Add(fileContent1);
                    //  content.Add(fileContent2);

                    // Make a call to Web API
                    var result = client.PostAsync("Company/Add", content).Result;

                    Console.WriteLine(result.StatusCode);
                    Console.ReadLine();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
            
        }

        // POST: Company/Create
        [HttpGet]
        public ActionResult Creates(CompnayModel compnayModel)
        {
            try
            {
                using (var client = new HttpClient())
                using (var content = new MultipartFormDataContent())
                {
                    // Make sure to change API address
                    client.BaseAddress = new Uri("http://itmolen-001-site8.htempurl.com/api/");

                    // Add first file content 
                    var fileContent1 = new ByteArrayContent(System.IO.File.ReadAllBytes(@"C:\Users\IT Molen\Pictures\pic 2.PNG"));
                    fileContent1.Headers.ContentDisposition = new ContentDispositionHeaderValue("LogoUrl")
                    {
                        FileName = "Sample.pdf"
                    };

                    fileContent1.Headers.Add("Hello", compnayModel.Name);

                    // Add Second file content
                    // var fileContent2 = new ByteArrayContent(System.IO.File.ReadAllBytes(@"c:\Users\aisadmin\Desktop\Sample.txt"));
                    // fileContent2.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    // {
                    //     FileName = "Sample.txt"
                    //  };

                    content.Add(fileContent1);
                    //  content.Add(fileContent2);

                    // Make a call to Web API
                    var result = client.PostAsync("Company/Add", content).Result;
                    
                    Console.WriteLine(result.StatusCode);
                    Console.ReadLine();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
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

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using TramaWebApp.Models;
using Microsoft.AspNet.Mvc.Rendering;

namespace TramaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            return View(_context.books.ToList());
        }
        public IActionResult SendMail()
        {
            return View();
        }

        
        public IActionResult Sent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendMail(MailModel _myMailModel)
        {
            System.Uri uri = new System.Uri("https://api.mailgun.net/v3");

            if (ModelState.IsValid)
            {
                
                RestClient client = new RestClient();
                client.BaseUrl = uri;
                client.Authenticator =
                       new HttpBasicAuthenticator("api",
                                                  "key-7e3b5a92c660532d094aa4936e35b5c0");
                RestRequest request = new RestRequest();
                request.AddParameter("domain",
                                    "sandbox9e305ca5453b416f98bc96488aee4749.mailgun.org", ParameterType.UrlSegment);
                request.Resource = "{domain}/messages";
                request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox9e305ca5453b416f98bc96488aee4749.mailgun.org>");
                request.AddParameter("to", "Aleksandrs Sumeiko <alexshumeiko@yahoo.com>");
           
                request.AddParameter("subject", "Hi Alex");

                var bodyFormat = "Email From: {0} ({1}), Phone: {2}, Message: {3} ";
                string body = string.Format(bodyFormat, _myMailModel.Name, _myMailModel.From, _myMailModel.Phone,_myMailModel.Body);

                request.AddParameter("text", body);
                request.Method = Method.POST;

                client.Execute(request);

                return RedirectToAction("Sent");
            }
            else
            {
                return View(_myMailModel);
            }
        }

     

        public IActionResult Error()
        {
            return View();
        }

    }
}

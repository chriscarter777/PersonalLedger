using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NLog;
using PersonalLedger.Data;
using PersonalLedger.Models;

namespace PersonalLedger.Controllers
{
    public class HomeController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IDataRepository _repo;

        public HomeController(HtmlEncoder htmlEncoder, IDataRepository repo)
        {
            _htmlEncoder = htmlEncoder;
            _repo = repo;
        }  //ctor

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage, bool redirect)
        {
            Debug.WriteLine("An error occurred in HomeController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            logger.Error("An error occurred in HomeController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException
        #endregion
    }  //controller
}  //namespace

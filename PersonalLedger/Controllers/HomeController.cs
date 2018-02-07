using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalLedger.Data;
using PersonalLedger.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace PersonalLedger.Controllers
{
    public class HomeController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private readonly ILogger _logger;
        private readonly IDataRepository _repo;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly string _userName;

        public HomeController(HtmlEncoder htmlEncoder, ILogger<HomeController> logger, IDataRepository repo, SignInManager<IdentityUser> signInManager)
        {
            _htmlEncoder = htmlEncoder;
            _logger = logger;
            _repo = repo;
            _signInManager = signInManager;
            _userName = signInManager.Context.User.Identity.Name;
        }  //ctor

        public IActionResult Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                _signInManager.SignOutAsync();
            }
            return View();
        }

        [Authorize]
        public IActionResult Main()
        {
            return View();
        }

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage, bool redirect)
        {
            _logger.LogError("{0}: An error occurred in HomeController/{1} for user: {2}.\n{3}\n{4}", DateTime.Now, method, _userName, e.Message, userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException

        public IActionResult Error()
        {
            //this method is the target of the --app.UseExceptionHandler("/Home/Error");-- middleware configured in Startup.cs, called for application exceptions
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        [Route("Home/403")]
        public IActionResult Status403()
        {
            //this method is the target of the --app.UseStatusCodePagesWithRedirects("/Home/{0}");-- middleware configured in Startup.cs
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View("403");
        }

        [Route("Home/404")]
        public IActionResult Status404()
        {
            //this method is the target of the --app.UseStatusCodePagesWithRedirects("/Home/{0}");-- middleware configured in Startup.cs
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View("404");
        }

        [Route("Home/500")]
        public IActionResult Status500()
        {
            //this method is the target of the --app.UseStatusCodePagesWithRedirects("/Home/{0}");-- middleware configured in Startup.cs
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View("500");
        }
        #endregion
    }  //controller
}  //namespace

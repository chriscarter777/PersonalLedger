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
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IDataRepository _repo;

        public UsersController(HtmlEncoder htmlEncoder, IDataRepository repo)
        {
            _htmlEncoder = htmlEncoder;
            _repo = repo;
        }  //ctor

        [HttpGet("[action]")]
        public async Task<IEnumerable<User>> UsersAsync()
        {
            Debug.WriteLine("Controller is requesting Users");
            return await _repo.GetUsersAsync();
        }

        [HttpGet("[action]")]
        public async Task AddUserAsync(User u)
        {
            await _repo.AddUserAsync(u);
        }

        [HttpGet("[action]")]
        public async Task DeleteUserAsync(string id)
        {
            await _repo.DeleteUserAsync(id);
        }

        [HttpGet("[action]")]
        public async Task UpdateUserAsync(User u)
        {
            await _repo.UpdateUserAsync(u);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage, bool redirect)
        {
            Debug.WriteLine("An error occurred in UsersController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            logger.Error("An error occurred in UsersController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException
        #endregion
    }  //controller
}  //namespace
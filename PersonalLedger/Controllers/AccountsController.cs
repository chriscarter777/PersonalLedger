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
    public class AccountsController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IDataRepository _repo;

        public AccountsController(HtmlEncoder htmlEncoder, IDataRepository repo)
        {
            _htmlEncoder = htmlEncoder;
            _repo = repo;
        }  //ctor

        [HttpGet("[action]")]
        public async Task<IEnumerable<Account>> AccountsAsync()
        {
            Debug.WriteLine("Controller is requesting Accounts");
            return await(_repo.GetAccountsAsync());
        }

        [HttpGet("[action]")]
        public async Task AddAccountAsync(Account a)
        {
            Debug.WriteLine("Controller is adding Account");
            await _repo.AddAccountAsync(a);
        }

        [HttpGet("[action]")]
        public async Task DeleteAccountAsync(int id)
        {
            await _repo.DeleteAccountAsync(id);
        }

        [HttpGet("[action]")]
        public async Task UpdateAccountAsync(Account a)
        {
            await _repo.UpdateAccountAsync(a);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage, bool redirect)
        {
            Debug.WriteLine("An error occurred in AccountsController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            logger.Error("An error occurred in AccountsController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException
        #endregion
    }  //controller
}  //namespace
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
    [Route("api/[controller]")]
    [Authorize]
    public class AccountsController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private readonly ILogger _logger;
        private readonly IDataRepository _repo;
        private readonly string _userName;

        public AccountsController(HtmlEncoder htmlEncoder, ILogger<AccountsController> logger, IDataRepository repo, SignInManager<IdentityUser> signInManager)
        {
            _htmlEncoder = htmlEncoder;
            _logger = logger;
            _repo = repo;
            _userName = signInManager.Context.User.Identity.Name;
        }  //ctor

        [HttpGet("[action]")]
        public async Task<IActionResult> AccountsAsync()
        {
            try
            {
                return Ok(await _repo.GetAccountsAsync());
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> AddAccountAsync(Account a)
        {
            //API returns the database ID of the added item
            try
            {
                return Ok(await _repo.AddAccountAsync(a));
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteAccountAsync(int id)
        {
            try
            {
                await _repo.DeleteAccountAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> UpdateAccountAsync(Account a)
        {
            try
            {
                await _repo.UpdateAccountAsync(a);
                return Ok();
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage, bool redirect)
        {
            _logger.LogError("{0}: An error occurred in AccountsController/{1} for user: {2}.\n{3}\n{4}", DateTime.Now, method, _userName, e.Message, userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException
        #endregion
    }  //controller
}  //namespace
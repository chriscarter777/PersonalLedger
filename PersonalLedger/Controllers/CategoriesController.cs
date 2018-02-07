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
    public class CategoriesController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private readonly ILogger _logger;
        private readonly IDataRepository _repo;
        private readonly string _userName;

        public CategoriesController(HtmlEncoder htmlEncoder, ILogger<CategoriesController> logger, IDataRepository repo, SignInManager<IdentityUser> signInManager)
        {
            _htmlEncoder = htmlEncoder;
            _logger = logger;
            _repo = repo;
            _userName = signInManager.Context.User.Identity.Name;
        }  //ctor

        [HttpGet("[action]")]
        public async Task<IActionResult> CategoriesAsync()
        {
            try
            {
                return Ok(await _repo.GetCategoriesAsync());
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> AddCategoryAsync(Category c)
        {
            //API returns the database ID of the added item
            try
            {
                return Ok(await _repo.AddCategoryAsync(c));
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                await _repo.DeleteCategoryAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                HandleException(e, ControllerContext.RouteData.Values["action"].ToString(), "", false);
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> UpdateCategoryAsync(Category c)
        {
            try
            {
                await _repo.UpdateCategoryAsync(c);
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
            _logger.LogError("{0}: An error occurred in CategoriesController/{1} for user: {2}.\n{3}\n{4}", DateTime.Now, method, _userName, e.Message, userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException
        #endregion
    }  //controller
}  //namespace
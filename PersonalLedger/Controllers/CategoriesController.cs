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
    public class CategoriesController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IDataRepository _repo;

        public CategoriesController(HtmlEncoder htmlEncoder, IDataRepository repo)
        {
            _htmlEncoder = htmlEncoder;
            _repo = repo;
        }  //ctor

        [HttpGet("[action]")]
        public async Task<IEnumerable<Category>> CategoriesAsync()
        {
            Debug.WriteLine("Controller is requesting Categories");
            return await _repo.GetCategoriesAsync();
        }

        [HttpGet("[action]")]
        public async Task AddCategoryAsync(Category c)
        {
            await _repo.AddCategoryAsync(c);
        }

        [HttpGet("[action]")]
        public async Task DeleteCategoryAsync(int id)
        {
            await _repo.DeleteCategoryAsync(id);
        }

        [HttpGet("[action]")]
        public async Task UpdateCategoryAsync(Category c)
        {
            await _repo.UpdateCategoryAsync(c);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage, bool redirect)
        {
            Debug.WriteLine("An error occurred in CategoriesController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            logger.Error("An error occurred in CategoriesController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException
        #endregion
    }  //controller
}  //namespace
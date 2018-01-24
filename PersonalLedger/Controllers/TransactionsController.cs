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
    public class TransactionsController : Controller
    {
        private HtmlEncoder _htmlEncoder;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IDataRepository _repo;

        public TransactionsController(HtmlEncoder htmlEncoder, IDataRepository repo)
        {
            _htmlEncoder = htmlEncoder;
            _repo = repo;
        }  //ctor

        [HttpGet("[action]")]
        public async Task<IEnumerable<Transaction>> TransactionsAsync()
        {
            Debug.WriteLine("Controller is requesting Transactions");
            return await _repo.GetTransactionsAsync();
        }

        [HttpGet("[action]")]
        public async Task AddTransactionAsync(Transaction t)
        {
            await _repo.AddTransactionAsync(t);
        }

        [HttpGet("[action]")]
        public async Task DeleteTransactionAsync(int id)
        {
            await _repo.DeleteTransactionAsync(id);
        }

        [HttpGet("[action]")]
        public async Task UpdateTransactionAsync(Transaction t)
        {
            await _repo.UpdateTransactionAsync(t);
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage, bool redirect)
        {
            Debug.WriteLine("An error occurred in TransactionsController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            logger.Error("An error occurred in TransactionsController/" + method + ".\n" + e.Message + ".\n" + userMessage);
            if (redirect)
            {
                RedirectToAction("Error");
            }
        }  //HandleException
        #endregion
    }  //controller
}  //namespace
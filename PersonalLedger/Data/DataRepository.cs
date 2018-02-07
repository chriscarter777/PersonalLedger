using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalLedger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PersonalLedger.Data
{
    public class DataRepository : IDataRepository
    {
        #region Fields
        private readonly LedgerDbContext _context;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly ILogger _logger;
        private readonly string _userName;
        private readonly bool _userSI;
        private readonly bool _userAdmin;
        private readonly List<Category> _defaultCategories = new List<Category>{
            //OTHER
            new Category
            {
                Name = "Adjustment",
                Tax = false,
                Type= "Other"
            },
            new Category
            {
                Name = "Transfer",
                Tax = false,
                Type= "Other"
            },
            //INCOME
            new Category
            {
                Name = "Salary",
                Tax = true,
                Type= "Income"
            },
            new Category
            {
                Name = "Bonus",
                Tax = true,
                Type= "Income"
            },
            new Category
            {
                Name = "Commission",
                Tax = true,
                Type= "Income"
            },
            new Category
            {
                Name = "Ordinary Dividend",
                Tax = true,
                Type= "Income"
            },
            new Category
            {
                Name = "Qualified Dividend",
                Tax = true,
                Type= "Income"
            },
            new Category
            {
                Name = "Interest Income",
                Tax = true,
                Type= "Income"
            },
            new Category
            {
                Name = "Gift Income",
                Tax = true,
                Type= "Income"
            },
            new Category
            {
                Name = "Other Income",
                Tax = true,
                Type= "Income"
            },
            //EXPENSE
            new Category
            {
                Name = "Automobile",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Business",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Children",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Clothing",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Eating Out",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Education",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Electricity",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Entertainment",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Fee",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Garbage",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Gasoline",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Gift Expense",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Grocery and Houseware",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Health and Grooming",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Home Maintenance",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Insurance",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Interest Expense",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Internet",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Legal",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Medical",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Mortgage Principal",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Mortgage Interest",
                Tax = true,
                Type= "Expense"
            },
            new Category
            {
                Name = "Natural Gas",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Other Expense",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Parking",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Pet",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Phone",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Rent",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Tax",
                Tax = true,
                Type= "Expense"
            },
            new Category
            {
                Name = "Television",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Travel",
                Tax = false,
                Type= "Expense"
            },
            new Category
            {
                Name = "Water and Sewer",
                Tax = false,
                Type= "Expense"
            }
        };
        #endregion

        public DataRepository(LedgerDbContext context, HtmlEncoder htmlEncoder, ILogger<DataRepository> logger, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _htmlEncoder = htmlEncoder;
            _logger = logger;
            _userName = signInManager.Context.User.Identity.Name;
            _userSI = signInManager.Context.User.Identity.IsAuthenticated;
            _userAdmin = signInManager.Context.User.IsInRole("Administrator");
        }  //ctor

        #region Accounts
        public async Task<Account[]> GetAccountsAsync()
        {
            _logger.LogTrace("DataRepository is getting Accounts for user: {0}.", _userName);
            try
            {
                return await _context.Accounts.Where(x => x.User == _userName).ToArrayAsync() ?? new Account[0];
            }
            catch (Exception e)
            {
                HandleException(e, nameof(GetAccountsAsync), "");
                return new Account[0];
            }
        }

        public async Task<int> AddAccountAsync(Account a)
        {
            _logger.LogTrace("DataRepository is adding Account {0} for user: {1}.", a.Name, _userName);
            try
            {
                _context.Accounts.Add(a);
                await _context.SaveChangesAsync();
                return a.ID;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(AddAccountAsync), "");
                return -1;
            }
        }

        public async Task<int> DeleteAccountAsync(int id)
        {
            _logger.LogTrace("DataRepository is deleting Account {0} for user: {1}.", id, _userName);
            try
            {
                _context.Accounts.Remove(_context.Accounts.Single(x => x.ID == id));
                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(DeleteAccountAsync), "");
                return -1;
            }
        }

        public async Task<int> UpdateAccountAsync(Account a)
        {
            _logger.LogTrace("DataRepository is updating Account {0} for user: {1}.", a.Name, _userName);
            try
            {
                Account toUpdate = _context.Accounts.SingleOrDefault(x => x.ID == a.ID);
                toUpdate = a;
                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(UpdateAccountAsync), "");
                return -1;
            }
        }
        #endregion
        #region Categories
        public async Task<Category[]> GetCategoriesAsync()
        {
            _logger.LogTrace("DataRepository is getting Categories for user: {0}.", _userName);
            try
            {
                if (_context.Categories.Where(x => x.User == _userName).Count() == 0)
                {
                    await SeedUserCategoriesAsync();
                }
                return await _context.Categories.Where(x => x.User == _userName).ToArrayAsync() ?? new Category[0];
            }
            catch (Exception e)
            {
                HandleException(e, nameof(GetCategoriesAsync), "");
                return new Category[0];
            }
        }

        public async Task<int> AddCategoryAsync(Category c)
        {
            _logger.LogTrace("DataRepository is adding Category {0} for user: {1}.", c.Name, _userName);
            try
            {
                _context.Categories.Add(c);
                await _context.SaveChangesAsync();
                return c.ID;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(AddCategoryAsync), "");
                return -1;
            }
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            _logger.LogTrace("DataRepository is deleting Category {0} for user: {1}.", id, _userName);
            try
            {
                _context.Categories.Remove(_context.Categories.Single(x => x.ID == id));
                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(DeleteCategoryAsync), "");
                return -1;
            }
        }

        public async Task<int> UpdateCategoryAsync(Category c)
        {
            _logger.LogTrace("DataRepository is updating Category {0} for user: {1}.", c.Name, _userName);
            try
            {
                Category toUpdate = _context.Categories.SingleOrDefault(x => x.ID == c.ID);
                toUpdate = c;
                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(UpdateCategoryAsync), "");
                return -1;
            }
        }

        private async Task SeedUserCategoriesAsync()
        {
            _logger.LogInformation("DataRepository is populating " + _userName + "'s default Categories for first use.");
            try
            {
                List<Category> seedCategories = new List<Category>(_defaultCategories).OrderByDescending(x => x.Type).ThenBy(x => x.Name).ToList();
                foreach (Category c in seedCategories)
                {
                    c.User = _userName;  //each user will begin with their own set of default categories
                }
                _context.Categories.AddRange(seedCategories);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                HandleException(e, nameof(SeedUserCategoriesAsync), "");
            }
        }
        #endregion
        #region Transactions
        public async Task<Transaction[]> GetTransactionsAsync()
        {
            _logger.LogTrace("DataRepository is getting Transactions for user: {0}.", _userName);
            try
            {
                return await _context.Transactions.Where(x => x.User == _userName).ToArrayAsync() ?? new Transaction[0];
            }
            catch (Exception e)
            {
                HandleException(e, nameof(GetTransactionsAsync), "");
                return new Transaction[0];
            }
        }

        public async Task<int> AddTransactionAsync(Transaction t)
        {
            _logger.LogTrace("DataRepository is adding Transaction {0}/category {1} for user: {2}.", t.Amount, t.Category, _userName);
            try
            {
                _context.Transactions.Add(t);
                await _context.SaveChangesAsync();
                return t.ID;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(AddTransactionAsync), "");
                return -1;
            }
        }

        public async Task<int> DeleteTransactionAsync(int id)
        {
            _logger.LogTrace("DataRepository is deleting Transaction {0} for user: {1}.", id, _userName);
            try
            {
                _context.Transactions.Remove(_context.Transactions.Single(x => x.ID == id));
                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(DeleteTransactionAsync), "");
                return -1;
            }
        }

        public async Task<int> UpdateTransactionAsync(Transaction t)
        {
            _logger.LogTrace("DataRepository is updating Transaction {0} for user: {1}.", t.ID, _userName);
            try
            {
                Transaction toUpdate = _context.Transactions.SingleOrDefault(x => x.ID == t.ID);
                toUpdate = t;
                await _context.SaveChangesAsync();
                return 0;
            }
            catch (Exception e)
            {
                HandleException(e, nameof(UpdateTransactionAsync), "");
                return -1;
            }
        }
        #endregion

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage)
        {
            _logger.LogError("{0}: An error occurred in DataRepository/{1} for user: {2}.\n{3}\n{4}", DateTime.Now, method, _userName, e.Message, userMessage);
        }  //HandleException
        #endregion
    }  //repository
}  //namespace

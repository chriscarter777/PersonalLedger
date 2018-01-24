using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalLedger.Models;
using NLog;

namespace PersonalLedger.Data
{
    public class DataRepository : IDataRepository
    {
        #region Fields
        private readonly DataContext _context;
        private readonly HtmlEncoder _htmlEncoder;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private readonly int _user = 1;  //for development, always use user 1
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

        public DataRepository(DataContext context, HtmlEncoder htmlEncoder)
        {
            _context = context;
            _htmlEncoder = htmlEncoder;
        }  //ctor

        #region Accounts
        public async Task<List<Account>> GetAccountsAsync()
        {
            Debug.WriteLine("DataRepository is getting Accounts.");
            return await _context.Accounts.Where(x => x.User == _user).ToListAsync()?? new List<Account>();
        }

        public async Task<int> AddAccountAsync(Account a)
        {
            Debug.WriteLine("DataRepository is adding Account.");
            _context.Accounts.Add(a);
            await _context.SaveChangesAsync();
            return a.ID;
        }

        public async Task<int> DeleteAccountAsync(int id)
        {
            Debug.WriteLine("DataRepository is deleting Account.");
            _context.Accounts.Remove(_context.Accounts.Single(x => x.ID == id));
            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<int> UpdateAccountAsync(Account a)
        {
            Debug.WriteLine("DataRepository is updating Account.");
            Account toUpdate = _context.Accounts.SingleOrDefault(x => x.ID == a.ID);
            toUpdate = a;
            await _context.SaveChangesAsync();
            return 0;
        }
        #endregion
        #region Categories
        public async Task<List<Category>> GetCategoriesAsync()
        {
            Debug.WriteLine("DataRepository is getting Categories.");
            if(_context.Categories.Where(x => x.User == _user).Count() == 0)
            {
                Debug.WriteLine("DataRepository is populating default Categories for first use.");
                List<Category> seedCategories = new List<Category>(_defaultCategories);
                foreach(Category c in seedCategories)
                {
                    c.User = _user;  //each user will begin with their own set of default categories
                }
                _context.Categories.AddRange(seedCategories);
                await _context.SaveChangesAsync();
            }
            return await _context.Categories.Where(x => x.User == _user).ToListAsync();
        }

        public async Task<int> AddCategoryAsync(Category c)
        {
            Debug.WriteLine("DataRepository is adding Category.");
            _context.Categories.Add(c);
            await _context.SaveChangesAsync();
            return c.ID;
        }

        public async Task<int> DeleteCategoryAsync(int id)
        {
            Debug.WriteLine("DataRepository is deleting Category.");
            _context.Categories.Remove(_context.Categories.Single(x => x.ID == id));
            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<int> UpdateCategoryAsync(Category c)
        {
            Debug.WriteLine("DataRepository is updating Category.");
            Category toUpdate = _context.Categories.SingleOrDefault(x => x.ID == c.ID);
            toUpdate = c;
            await _context.SaveChangesAsync();
            return 0;
        }
        #endregion
        #region Transactions
        public async Task<List<Transaction>> GetTransactionsAsync()
        {
            Debug.WriteLine("DataRepository is getting Transactions.");
            return await _context.Transactions.Where(x => x.User == _user).ToListAsync();
        }

        public async Task<int> AddTransactionAsync(Transaction t)
        {
            Debug.WriteLine("DataRepository is adding Transaction.");
            _context.Transactions.Add(t);
            await _context.SaveChangesAsync();
            return t.ID;
        }

        public async Task<int> DeleteTransactionAsync(int id)
        {
            Debug.WriteLine("DataRepository is deleting Transaction.");
            _context.Transactions.Remove(_context.Transactions.Single(x => x.ID == id));
            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<int> UpdateTransactionAsync(Transaction t)
        {
            Debug.WriteLine("DataRepository is updating Transaction.");
            Transaction toUpdate = _context.Transactions.SingleOrDefault(x => x.ID == t.ID);
            toUpdate = t;
            await _context.SaveChangesAsync();
            return 0;
        }
        #endregion
        #region Users
        public async Task<List<User>> GetUsersAsync()
        {
            Debug.WriteLine("DataRepository is getting Users.");
            return await _context.Users.ToListAsync() ?? new List<User>();
        }

        public async Task<string> AddUserAsync(User u)
        {
            Debug.WriteLine("DataRepository is adding User.");
            _context.Users.Add(u);
            await _context.SaveChangesAsync();
            return u.Id;
        }

        public async Task<int> DeleteUserAsync(string id)
        {
            Debug.WriteLine("DataRepository is deleting User.");
            _context.Users.Remove(_context.Users.Single(x => x.Id == id));
            await _context.SaveChangesAsync();
            return 0;
        }

        public async Task<int> UpdateUserAsync(User u)
        {
            Debug.WriteLine("DataRepository is updating User.");
            User toUpdate = _context.Users.SingleOrDefault(x => x.Id == u.Id);
            toUpdate = u;
            await _context.SaveChangesAsync();
            return 0;
        }
        #endregion

        #region Infrastructure
        private void HandleException(Exception e, string method, string userMessage)
        {
            Debug.WriteLine("An error occurred in DataRepository/" + method + ".\n" + e.Message + ".\n" + userMessage);
            logger.Error("An error occurred in DataRepository/" + method + ".\n" + e.Message + ".\n" + userMessage);
        }  //HandleException
        #endregion
    }  //repository
}  //namespace

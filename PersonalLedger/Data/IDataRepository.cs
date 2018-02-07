using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalLedger.Models;

namespace PersonalLedger.Data
{
    public interface IDataRepository
    {
        Task<Account[]> GetAccountsAsync();
        Task<int> AddAccountAsync(Account a);
        Task<int> DeleteAccountAsync(int id);
        Task<int> UpdateAccountAsync(Account a);

        Task<Category[]> GetCategoriesAsync();
        Task<int> AddCategoryAsync(Category c);
        Task<int> DeleteCategoryAsync(int id);
        Task<int> UpdateCategoryAsync(Category c);

        Task<Transaction[]> GetTransactionsAsync();
        Task<int> AddTransactionAsync(Transaction t);
        Task<int> DeleteTransactionAsync(int id);
        Task<int> UpdateTransactionAsync(Transaction t);
    }  //interface
}  //namespace

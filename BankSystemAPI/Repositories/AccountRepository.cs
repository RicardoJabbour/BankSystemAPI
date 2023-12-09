using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Models;
using BankSystemAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankSystemAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly BankDbContext _dbContext;

        public AccountRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account GetAccountById(int accountId)
        {
            var account = _dbContext.Accounts.FirstOrDefault(x => x.AccountId == accountId);
            
            return account;
        }

        public void AddAccount(Account account)
        {
            if (account == null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            _dbContext.Accounts.Add(account);

            _dbContext.SaveChanges();
        }

        public bool UpdateAccount(Account account)
        {
            _dbContext.Accounts.Update(account);

            _dbContext.SaveChanges();
            
            return true;
        }

        public List<Account> GetCustomerAccounts(int customerId)
        {
            var accounts = _dbContext.Accounts
                           .Include(x => x.Customer)
                           .Where(x => x.CustomerId == customerId).ToList();

            return accounts;
        }

        public List<Account> GetAllAccounts(int accountId)
        {
            var accounts = _dbContext.Accounts
                           .Include (x => x.Customer)
                           .Where(x => x.AccountId != accountId).ToList();

            return accounts;
        }

        public decimal GetAccountLimit(int accountId)
        {
            var limit = _dbContext.Accounts.Where(x => x.AccountId == accountId).Select(x => x.Balance).FirstOrDefault();

            return limit;
        }
    }
}

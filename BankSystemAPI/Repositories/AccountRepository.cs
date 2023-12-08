using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Models;
using BankSystemAPI.Repositories.Interfaces;

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

            // You may want to perform additional validation or business logic here

            // Add the customer entity to the database context
            _dbContext.Accounts.Add(account);

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        public bool UpdateAccount(Account account)
        {
            _dbContext.Accounts.Update(account);

            _dbContext.SaveChanges();
            
            return true;
        }
    }
}

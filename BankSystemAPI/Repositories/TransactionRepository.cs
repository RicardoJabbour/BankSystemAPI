using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Models;
using BankSystemAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankSystemAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly BankDbContext _dbContext;

        public TransactionRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool AddTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                return false;

                throw new ArgumentNullException(nameof(transaction));
            }

            _dbContext.Transactions.Add(transaction);

            _dbContext.SaveChanges();

            return true;
        }

        public List<Transaction> GetAllTransactionsByCustomerId(int customerId)
        {
            var acountsIds = _dbContext.Accounts.Where(x => x.CustomerId == customerId).Select(x => x.AccountId).ToList();
            var transactions = new List<Transaction>();

            foreach (var acountId in acountsIds)
            {
                var transactionsAccount = _dbContext.Transactions.Where(x => x.AccountId == acountId).ToList();
                transactions.AddRange(transactionsAccount);
            }

            transactions = transactions.OrderBy(x => x.TransactionDate).ToList();

            return transactions;
        }
    }
}

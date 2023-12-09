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

        public Transaction GetTransactionById(int transactionId)
        {
            return new Transaction();
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }

            // You may want to perform additional validation or business logic here

            // Add the customer entity to the database context
            _dbContext.Transactions.Add(transaction);

            // Save changes to the database
            _dbContext.SaveChanges();
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

            return transactions;
        }
    }
}

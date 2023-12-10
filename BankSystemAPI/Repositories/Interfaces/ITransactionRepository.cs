using BankSystemAPI.Data.Models.Entities;

namespace BankSystemAPI.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        bool AddTransaction(Transaction transaction);
        List<Transaction> GetAllTransactionsByCustomerId(int customerId);
    }
}

using BankSystemAPI.Data.Models.Entities;

namespace BankSystemAPI.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Transaction GetTransactionById(int transactionId);
        void AddTransaction(Transaction transaction);
    }
}

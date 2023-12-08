using BankSystemAPI.Data.Models.Entities;

namespace BankSystemAPI.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccountById(int accountId);
        void AddAccount(Account account);
        bool UpdateAccount(Account account);
    }
}

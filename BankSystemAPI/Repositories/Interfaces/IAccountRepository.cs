using BankSystemAPI.Data.Models.Entities;

namespace BankSystemAPI.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Account GetAccountById(int accountId);
        void AddAccount(Account account);
        bool UpdateAccount(Account account);
        List<Account> GetCustomerAccounts(int customerId);
        List<Account> GetAllAccounts(int accountId);
        decimal GetAccountLimit(int accountId);
    }
}

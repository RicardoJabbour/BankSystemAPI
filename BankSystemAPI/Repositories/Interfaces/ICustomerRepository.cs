using BankSystemAPI.Data.Models.Entities;

namespace BankSystemAPI.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int customerId);
        //Customer GetCustomerByAccount(int accountId);
        Customer GetCustomerInfoById(int customerId);
        void AddCustomer(Customer customer);
    }
}

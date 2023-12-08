using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Models;
using BankSystemAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BankSystemAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BankDbContext _dbContext;

        public CustomerRepository(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer GetCustomerInfoById(int customerId)
        {
            var customer = _dbContext.Customers
                           .Include(c => c.Accounts) // Include the associated accounts
                           .ThenInclude(a => a.Transactions)
                           .FirstOrDefault(x => x.CustomerId == customerId);

            return customer;
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            // You may want to perform additional validation or business logic here

            // Add the customer entity to the database context
            _dbContext.Customers.Add(customer);

            // Save changes to the database
            _dbContext.SaveChanges();
        }

        public Customer GetCustomerById(int customerId)
        {
            var customer = _dbContext.Customers
                           .Include(c => c.Accounts)
                           .FirstOrDefault(x => x.CustomerId == customerId);

            return customer;
        }

        //public Customer GetCustomerByAccount(int accountId)
        //{
        //    var customer = _dbContext.Customers
        //                   .Include(c => c.Accounts)
        //                   .FirstOrDefault(x => x.CustomerId.Equals(
        //                                        _dbContext.Accounts
        //                                        .FirstOrDefault(a => a.AccountId == accountId).CustomerId));

        //    return customer;
        //}
    }
}

using AutoMapper;
using BankSystemAPI.Data.Models.DTOs;
using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Models;
using BankSystemAPI.Repositories;
using BankSystemAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public AccountsController(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult OpenAccount([FromBody] AccountDTO accountDTO)
        {
            try
            {
                // Check if the customer exists
                var customer = _customerRepository.GetCustomerById(accountDTO.CustomerId);

                if (customer == null)
                {
                    return NotFound($"Customer with ID {accountDTO.CustomerId} not found.");
                }

                // Create a new account
                var account = _mapper.Map<Account>(accountDTO);

                _accountRepository.AddAccount(account);

                // If initialCredit is not 0, create a transaction
                if (accountDTO.Balance != 0)
                {
                    var transaction = new Transaction
                    {
                        AccountId = account.AccountId,
                        Amount = account.Balance,
                        TransactionDate = DateTime.UtcNow
                    };

                    _transactionRepository.AddTransaction(transaction);
                }

                return Ok($"Account for Customer ID {accountDTO.CustomerId} opened successfully.");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it according to your needs
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }
}

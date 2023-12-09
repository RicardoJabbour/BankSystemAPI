using AutoMapper;
using BankSystemAPI.Data.Models.DTOs;
using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystemAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public TransactionController(ICustomerRepository customerRepository, IMapper mapper, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        //[HttpPost]
        //public IActionResult MakeTransaction(TransactionType transactionType, ) { }

        [HttpPost]
        public IActionResult MakeTransaction1(int customerId, int accountId, int amount)
        {
            try
            {
                var customer = _customerRepository.GetCustomerById(customerId);

                if(customer ==  null)
                {
                    return NotFound($"Customer with ID {customerId} not found.");
                }
                else 
                {
                    var accountToAddTo = _accountRepository.GetAccountById(accountId);

                    if(accountToAddTo == null)
                    {
                        return NotFound($"Account with ID {accountId} not found.");
                    }
                    else
                    {
                        var transactionDTO = new TransactionDTO
                        {
                            Amount = amount,
                            TransactionDate = DateTime.UtcNow,
                            AccountId = accountId,
                        };

                        var transaction = _mapper.Map<Transaction>(transactionDTO);

                        _transactionRepository.AddTransaction(transaction);

                        var customerAccount = customer.Accounts.FirstOrDefault(x => x.AccountId == accountId);

                        if (customerAccount?.AccountId == accountId) 
                        {
                            customerAccount.Balance = customerAccount.Balance + amount;
                            _accountRepository.UpdateAccount(customerAccount);
                        }
                        else
                        {

                            var acountToRemoveFrom = customer.Accounts.FirstOrDefault(x => x.CustomerId == customerId);
                            acountToRemoveFrom.Balance = acountToRemoveFrom.Balance - amount;
                            _accountRepository.UpdateAccount(acountToRemoveFrom);


                            accountToAddTo.Balance = accountToAddTo.Balance + amount;
                            _accountRepository.UpdateAccount(accountToAddTo);
                        }

                        return Ok("Transaction Created");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetTransactionsByCustomerId")]
        public IActionResult GetTransactionsByCustomerId(int customerId)
        {
            try
            {
                var transactipons = _transactionRepository.GetAllTransactionsByCustomerId(customerId);

                var transactiponsDto = _mapper.Map<List<TransactionDTO>>(transactipons);

                return Ok(transactiponsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

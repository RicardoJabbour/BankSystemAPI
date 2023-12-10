using AutoMapper;
using BankSystemAPI.Data.Models.DTOs;
using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

namespace BankSystemAPI.Controllers
{
    [Route("api/[Controller]/")]
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

        [HttpPost("MakeTransaction")]
        public IActionResult MakeTransaction(List<TransactionDTO> transactions)
        {
            try
            {
                if (transactions[0].TransactionType == TransactionType.Deposit)
                {
                    var accountToAddTo = _accountRepository.GetAccountById(transactions[0].AccountId);
                    accountToAddTo.Balance = accountToAddTo.Balance + transactions[0].Amount;
                    var accountTo = _accountRepository.UpdateAccount(accountToAddTo);

                    var newTransaction = _mapper.Map<Transaction>(transactions[0]);

                    var _transactionFrom = _transactionRepository.AddTransaction(newTransaction);

                    return Ok(_transactionFrom);
                }
                else if (transactions[0].TransactionType == TransactionType.Withdraw)
                {
                    var accountToRetreveFrom = _accountRepository.GetAccountById(transactions[0].AccountId);
                    accountToRetreveFrom.Balance = accountToRetreveFrom.Balance - transactions[0].Amount;
                    
                    if(accountToRetreveFrom.Balance < 0)
                    {
                        return BadRequest("You can't make this transaction");
                    }

                    var accountTo = _accountRepository.UpdateAccount(accountToRetreveFrom);

                    var newTransaction = _mapper.Map<Transaction>(transactions[0]);

                    var _transactionFrom = _transactionRepository.AddTransaction(newTransaction);

                    return Ok(_transactionFrom);
                }
                else if(transactions[0].TransactionType == TransactionType.Transfer && transactions[1] != null){

                    var accountToRetreveFrom = _accountRepository.GetAccountById(transactions[0].AccountId);
                    accountToRetreveFrom.Balance = accountToRetreveFrom.Balance - transactions[0].Amount;

                    if (accountToRetreveFrom.Balance < 0)
                    {
                        return BadRequest("You can't make this transaction");
                    }

                    var accountfrom = _accountRepository.UpdateAccount(accountToRetreveFrom);

                    var newTransactionFrom = _mapper.Map<Transaction>(transactions[0]);

                    var _transactionFrom = _transactionRepository.AddTransaction(newTransactionFrom);

                    var accountToAddTo = _accountRepository.GetAccountById(transactions[1].AccountId);
                    accountToAddTo.Balance = accountToAddTo.Balance + transactions[1].Amount;
                    var accountTo = _accountRepository.UpdateAccount(accountToAddTo);

                    var newTransactionTo = _mapper.Map<Transaction>(transactions[1]);

                    var _transactionTo = _transactionRepository.AddTransaction(newTransactionTo);

                    return Ok(_transactionTo && _transactionFrom);
                }
                else
                {
                    return BadRequest();
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

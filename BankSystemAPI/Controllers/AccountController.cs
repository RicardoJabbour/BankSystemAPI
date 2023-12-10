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
    [Route("api/[controller]/")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;

        public AccountController(IAccountRepository accountRepository, ITransactionRepository transactionRepository, ICustomerRepository customerRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// The API will expose an endpoint which accepts the user information (customerID, initialCredit).
        /// Once the endpoint is called, a new account will be opened connected to the user whose ID is customerID.
        //  Also, if initialCredit is not 0, a transaction will be sent to the new account.
        /// </summary>
        /// <param name="accountDTO"></param>
        /// <returns></returns>
        [HttpPost("OpenAccount")]
        public IActionResult OpenAccount([FromBody] AccountDTO accountDTO)
        {
            try
            {
                var account = _mapper.Map<Account>(accountDTO);

                _accountRepository.AddAccount(account);

                if (accountDTO.Balance != 0)
                {
                    var transaction = new Transaction
                    {
                        AccountId = account.AccountId,
                        Amount = account.Balance,
                        TransactionDate = DateTime.UtcNow,
                        TransactionType = TransactionType.Deposit
                    };

                    _transactionRepository.AddTransaction(transaction);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetCustomerAccounts")]
        public IActionResult GetCustomerAccounts(int customerId)
        {
            try
            {
                var accounts = _accountRepository.GetCustomerAccounts(customerId);

                var accountsDto = _mapper.Map<List<AccountDTO>>(accounts);

                return Ok(accountsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllAccounts")]
        public IActionResult GetAllAccounts(int accountId)
        {
            try
            {
                var accounts = _accountRepository.GetAllAccounts(accountId);
            
                var accountsDto = _mapper.Map<List<AccountDTO>>(accounts);

                return Ok(accountsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("GetAccountLimit")]
        public IActionResult GetAccountLimit(int accountId)
        {
            try
            {
                var limit = _accountRepository.GetAccountLimit(accountId);

                return Ok(limit);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

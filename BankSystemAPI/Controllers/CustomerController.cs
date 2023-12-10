using AutoMapper;
using BankSystemAPI.Data.Models.DTOs;
using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankSystemAPI.Controllers
{
    [Route("api/[Controller]/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        [HttpPost("CreateCustomer")]
        public IActionResult CreateCustomer([FromBody] CustomerDTO customerDTO)
        {
            try
            {
                if (customerDTO == null)
                {
                    return BadRequest("Invalid input data");
                }
                else if (String.IsNullOrEmpty(customerDTO.FirstName) || String.IsNullOrEmpty(customerDTO.LastName) || String.IsNullOrEmpty(customerDTO.Email))
                {
                    return BadRequest("All Fields are required");
                }

                var newCustomer = _mapper.Map<Customer>(customerDTO);

                _customerRepository.AddCustomer(newCustomer);

                return Ok(true);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Another Endpoint will output the user information showing Name, Surname, balance, and transactions of the accounts.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("GetCustomerInfo")]
        public IActionResult GetCustomerInfo(int customerId)
        {
            try
            {
                var customer = _customerRepository.GetCustomerInfoById(customerId);

                var customerDto = _mapper.Map<CustomerDTO>(customer);

                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            try
            {
                var customers = _customerRepository.GetAllCustomers();

                var customersDto = _mapper.Map<List<CustomerDTO>>(customers);

                return Ok(customersDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

    }
}

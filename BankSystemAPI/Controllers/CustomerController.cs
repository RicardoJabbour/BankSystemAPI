using AutoMapper;
using BankSystemAPI.Data.Models.DTOs;
using BankSystemAPI.Data.Models.Entities;
using BankSystemAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BankSystemAPI.Controllers
{
    [Route("api/Customer")]
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

        [HttpPost("/CreateCustomer")]
        public IActionResult CreateCustomer([FromBody] CustomerDTO createCustomerDTO)
        {
            try
            {
                if (createCustomerDTO == null)
                {
                    return BadRequest("Invalid input data");
                }

                // Map the DTO to the Customer entity
                var newCustomer = _mapper.Map<Customer>(createCustomerDTO);

                // Add the customer to the repository
                _customerRepository.AddCustomer(newCustomer);

                // You may want to handle exceptions and provide appropriate responses

                return Ok("Customer created successfully");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("api/Customer/GetCustomerInfo")]
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

        [HttpGet("/api/Customer/GetAllCustomers")]
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

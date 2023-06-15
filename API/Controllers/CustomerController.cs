using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CustomerController : BaseApiController
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost("addCustomer")]
        public async Task<ActionResult<ResponseDto>> AddCustomer(CustomerRequestDto requestDto)
        {
            var response = await _customerService.AddCustomer(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }

        [HttpPost("updateCustomer")]
        public async Task<ActionResult<ResponseDto>> UpdateCustomer(CustomerUpdateRequestDto requestDto)
        {
            var response = await _customerService.EditCustomer(requestDto);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }
        [HttpPost("deleteCustomer/{CustomerId}")]
        public async Task<ActionResult<ResponseDto>> DeleteCustomer(int CustomerId)
        {
            var response = await _customerService.DeleteCustomer(CustomerId);
            if (!response.IsSuccess) return BadRequest(response.Message);
            else return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<Customer>> GetAllCustomers()
        {
            var response = await _customerService.GetAllCustomers();
            return Ok(response);
        }

        [HttpGet("{CustomerId}")]
        public async Task<ActionResult<Customer>> GetCustomer(int CustomerId)
        {
            var response = await _customerService.GetCustomerById(CustomerId);
            return Ok(response);
        }

    }
}

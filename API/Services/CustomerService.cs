using AutoMapper;
using API.DTOs;
using API.Entities;
using API.Interfaces;

namespace API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
        }

        public async Task<ResponseDto> AddCustomer(CustomerRequestDto customerRequestDto)
        {
            var responseDto = new ResponseDto();
            if (_customerRepository.CustomerExists(customerRequestDto.CustomerEmailAddress))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Customer Exists";
                return responseDto;
            }

            var customer = new Customer();
            _mapper.Map(customerRequestDto, customer);
            if (await _customerRepository.AddCustomerAsync(customer))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Customer added successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to add Customer";
            return responseDto;

        }

        public async Task<ResponseDto> EditCustomer(CustomerUpdateRequestDto customerRequestDto)
        {
            var responseDto = new ResponseDto();

            var customerResponse = await _customerRepository.GetCustomerByIdAsync(customerRequestDto.CustomerID);
            if (customerResponse == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Customer Does Not Exist";
                return responseDto;
            }
            if (customerResponse.CustomerEmailAddress.Trim() != customerRequestDto.CustomerEmailAddress.Trim())
            {
                if (_customerRepository.CustomerExists(customerRequestDto.CustomerEmailAddress.Trim()))
                {
                    responseDto = new ResponseDto();
                    responseDto.IsSuccess = false;
                    responseDto.Message = "Customer Exist";
                    return responseDto;
                }
            }
            var customer = new Customer();
            _mapper.Map(customerRequestDto, customer);
            customer.DateAdded = customerResponse.DateAdded;
            if (await _customerRepository.UpdateCustomerAsync(customer))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Customer edited successfully";
                return responseDto;
            }
            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to edit Customer";
            return responseDto;

        }

        public async Task<ResponseDto> DeleteCustomer(int customerId)
        {
            var responseDto = new ResponseDto();

            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = false;
                responseDto.Message = "Customer Does Not Exist";
                return responseDto;
            }

            customer.IsDeleted = true;
            customer.DateDeleted = DateTime.Now;

            if (await _customerRepository.UpdateCustomerAsync(customer))
            {
                responseDto = new ResponseDto();
                responseDto.IsSuccess = true;
                responseDto.Message = "Customer deleted successfully";
                return responseDto;
            }

            responseDto = new ResponseDto();
            responseDto.IsSuccess = false;
            responseDto.Message = "Failed to delete Customer";
            return responseDto;

        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var Customers = await _customerRepository.GetAllCustomersAsync();
            return Customers;
        }
        public async Task<Customer> GetCustomerById(int Id)
        {
            var Customer = await _customerRepository.GetCustomerByIdAsync(Id);
            return Customer;
        }
    }
}

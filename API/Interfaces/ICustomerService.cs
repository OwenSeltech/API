using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ICustomerService
    {
        Task<ResponseDto> AddCustomer(CustomerRequestDto customerRequestDto);
        Task<ResponseDto> EditCustomer(CustomerUpdateRequestDto customerRequestDto);
        Task<IEnumerable<Customer>> GetAllCustomers();
        Task<Customer> GetCustomerById(int Id);
        Task<ResponseDto> DeleteCustomer(int CustomerId);
    }
}

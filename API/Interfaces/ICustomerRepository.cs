using API.Entities;

namespace API.Interfaces
{
    public interface ICustomerRepository
    {
        Task<bool> AddCustomerAsync(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        public bool CustomerExists(string email);
    }
}

using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DataContext _context;
        public CustomerRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCustomerAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.Customers.Where(x => x.IsDeleted == false).ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers
                .Where(x => x.CustomerId == id)
                .Where(x => x.IsDeleted == false)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        
        public bool CustomerExists(string email)
        {
            return _context.Customers.Any(o => o.CustomerEmailAddress.ToLower().Trim() == email.ToLower().Trim());
        }
    }
}

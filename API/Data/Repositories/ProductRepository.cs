using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> AddProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Added;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Customer)
                .ToListAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Where(x => x.ProductId == id)
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByCustIdAsync(int id)
        {
            return await _context.Products
                .Where(x => x.CustomerId == id)
                .Where(x => x.IsDeleted == false)
                .Include(x => x.Customer)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}

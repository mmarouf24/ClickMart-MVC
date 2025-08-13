using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IProductRepo
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
        Task<Product> EditAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
    public class ProductRepo : IProductRepo
    {
        ECommerceDbContext _context=new ECommerceDbContext();
        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await Save();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await Save();
                return true;
            }
            return false;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(p=>p.Category).FirstOrDefaultAsync(p => p.ProductId== id);
            return product;
        }

        public async Task<Product> EditAsync(Product product)
        {
             _context.Products.Update(product);
            await Save();
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
             return await _context.Products.Include(p => p.Category).ToListAsync();
        }
         async Task Save()=>await _context.SaveChangesAsync();

        

    }
}

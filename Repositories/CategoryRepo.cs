using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface ICategoryRepo
    {
        Task<List<Category>> GetAllAsync();
    }
    public class CategoryRepo : ICategoryRepo
    {
        ECommerceDbContext _context;
        public CategoryRepo(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}

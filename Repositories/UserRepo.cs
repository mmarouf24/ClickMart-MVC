using ECommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IUserRepo
    {
        Task<List<User>> GetAllAsync();
        Task<User> Register(User user);

    }
    public class UserRepo : IUserRepo
    {
        ECommerceDbContext _context=new ECommerceDbContext();

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> Register(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}

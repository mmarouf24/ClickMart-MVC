using ECommerce.Models;
using ECommerce.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Repositories
{
    public interface IUserRepo
    {
        Task<List<User>> GetAllAsync();
        Task<User> Register(User user);
        Task<User> Login(LoginViewModel model);

    }
    public class UserRepo : IUserRepo
    {
        ECommerceDbContext _context;
        public UserRepo(ECommerceDbContext context)
        {
            _context = context;
        }
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
        public async Task<User> Login(LoginViewModel model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email && u.Password==model.Password);
            return user;
        }

    }
}

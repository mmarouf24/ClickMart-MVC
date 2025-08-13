using ECommerce.Models;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{

    public class UsersController : Controller
    {
        IUserRepo userRepo;
        public UsersController(IUserRepo _userRepo)
        {
            userRepo = _userRepo;
        }
        public async Task<IActionResult> Index()
        {
            var users=await userRepo.GetAllAsync();
            return View(users);
        }

        public IActionResult Register() {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid) {
                await userRepo.Register(user);
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
            
        }

        public async Task<IActionResult> CheckEmailExistance(string Email) {

            var users = await userRepo.GetAllAsync();
            var user=users.FirstOrDefault(u=>u.Email == Email);
            if (user != null)
                return Json(false);
            return Json(true);
        }

    }
}

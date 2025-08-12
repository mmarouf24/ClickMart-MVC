using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Controllers
{

    public class UsersController : Controller
    {
        ECommerceDbContext _context=new ECommerceDbContext();
        public IActionResult Index()
        {
            var users=_context.Users.ToList();
            return View(users);
        }

        public IActionResult Register() {
            return View();
        }
        [HttpPost]

        public IActionResult Register(User user)
        {
            if (ModelState.IsValid) {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(user);
            }
            
        }

        public IActionResult CheckEmailExistance(string Email) { 
        
            var user=_context.Users.FirstOrDefault(u=>u.Email == Email);
            if (user != null)
                return Json(false);
            return Json(true);
        }

    }
}

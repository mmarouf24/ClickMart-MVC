using System.Security.Claims;
using ECommerce.Models;
using ECommerce.Repositories;
using ECommerce.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        
        public IActionResult Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl=returnUrl;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl=null)
        {
            if (!ModelState.IsValid) return View(model);
            
                var user = await userRepo.Login(model);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid Email or Password!");
                    ViewBag.ReturnUrl = returnUrl;

                return View(model);
                }
                Claim c1 = new Claim(ClaimTypes.Name,user.FirstName +" "+ user.LastName);
                Claim c2= new Claim(ClaimTypes.Email,user.Email);
                Claim c3 = new Claim(ClaimTypes.Role, user.Role.ToString());
                ClaimsIdentity ci=new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                ci.AddClaim(c1);
                ci.AddClaim(c2);
                ci.AddClaim(c3);
                ClaimsPrincipal cp = new ClaimsPrincipal();
                cp.AddIdentity(ci);

                await HttpContext.SignInAsync(cp);
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
            return RedirectToAction("Index", "Home");
            
        }
        public async Task<IActionResult> LogOut()
        {
           await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register() {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) {
                User user = new User()
                {
                    FirstName= model.FirstName,
                    LastName= model.LastName,
                    Email= model.Email,
                    Phone= model.Phone,
                    Country= model.Country,
                    City= model.City,
                    Address = model.Address,
                    CreatedAt= DateTime.Now,
                    Password=model.Password,
                    
                };
                await userRepo.Register(user);
                return RedirectToAction("Login");
            }
            else
            {
                return View(model);
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

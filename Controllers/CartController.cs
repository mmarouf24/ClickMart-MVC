using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class CartController : Controller
    {
        ECommerceDbContext _context=new ECommerceDbContext();
        //cart/index/2
        public IActionResult Index(int id)
        {
            var cart = _context.Carts.Include(c => c.CartItems)
                .ThenInclude(ci =>  ci.Product)
                .FirstOrDefault(x => x.UserId ==  id);

           
            return View(cart);
            

        }
        public IActionResult AddtoCart(int id,int productId,int Qty) {
            var cart = _context.Carts.FirstOrDefault(c => c.UserId == id);
            if (cart == null) {
                cart = new Cart()
                {
                    UserId = id,
                    UpdatedAt = DateTime.Now
                };
                _context.Carts.Add(cart);
                _context.SaveChanges();

            }
            var cartItem =  _context.CartItems.FirstOrDefault(ci => ci.CartId == cart.CartId && ci.ProductId == productId);
            if (cartItem != null) {
                cartItem.Quantity += Qty;
            }
            else
            {
                cartItem = new CartItem
                {
                    CartId = cart.CartId,
                    ProductId = productId,
                    Quantity = Qty,
                    UnitPrice=_context.Products.FirstOrDefault(p=>p.ProductId == productId).Price
                };
                _context.CartItems.Add(cartItem);

            }
            _context.SaveChanges();
            return RedirectToAction("Index", new { id = id });


        }

        public IActionResult Remove(int CartItemId, int UserId)
        {
            var cartItem =  _context.CartItems.Find(CartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                 _context.SaveChanges();
            }

            return RedirectToAction("Index", new { id = UserId });
        }

    }

   
}

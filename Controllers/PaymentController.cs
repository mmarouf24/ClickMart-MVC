using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class PaymentController : Controller
    {
        ECommerceDbContext _context=new ECommerceDbContext();
        public IActionResult Index()
        {
            var orders = _context.Orders.Include(o=>o.User).ToList();
            return View(orders);
        }
        //id=> cart id
        public IActionResult Add(int? id) {
            if (id == null || id == 0) return BadRequest();
           
            var cart =_context.Carts.Include(c=>c.CartItems).FirstOrDefault(c=>c.CartId==id);
            var order = new Order()
            {
                UserId = cart.UserId,
                Status = OrderStatus.Pending,
                TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.UnitPrice),
                
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach(var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem()
                {
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice
                };
                var product = _context.Products.FirstOrDefault(p => p.ProductId == cartItem.ProductId);
               
                product.StockQty-=cartItem.Quantity;
                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}

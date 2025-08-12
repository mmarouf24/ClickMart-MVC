using ECommerce.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class ProductsController : Controller
    {
        ECommerceDbContext _context=new ECommerceDbContext();
        public IActionResult Index()
        {
            var products=_context.Products.Include(p=>p.Category).ToList();
            return View(products);
        }
        public IActionResult Add()
        {
            ViewBag.categories = _context.Categories.ToList();

            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product) {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categories = _context.Categories.ToList();
            return View(product);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            else if(id==0) return BadRequest();
                var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            if(product == null) return NotFound();
            ViewBag.categories = _context.Categories.ToList();

            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(int id, Product product) {
            if (ModelState.IsValid)
            {
                product.ProductId = id;
                _context.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categories = _context.Categories.ToList();

            return View(product);
        }

        public IActionResult Delete(int? id) {
            if (id == null) return BadRequest();
            else if (id == 0) return BadRequest();
            var product = _context.Products.FirstOrDefault(p=>p.ProductId==id);
            if(product == null) return NotFound();
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Details(int? id) {
            if (id == null) return BadRequest();
            else if (id == 0) return BadRequest();
            var product = _context.Products.Include(p=>p.Category).FirstOrDefault(p => p.ProductId == id);
            return View(product);
        }
    }
}

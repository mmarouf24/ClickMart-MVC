using ECommerce.Models;
using ECommerce.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Controllers
{
    public class ProductsController : Controller
    {
        IProductRepo productRepo;
        ICategoryRepo categoryRepo;

        public ProductsController(IProductRepo _productRepo,ICategoryRepo _categoryRepo)
        {
            productRepo = _productRepo;
            categoryRepo = _categoryRepo;
        }
        public async Task<IActionResult> Index()
        {
            var products=await productRepo.GetAllAsync();
            return View(products);
        }
        public async Task<IActionResult> Add()
        {
            ViewBag.categories = await categoryRepo.GetAllAsync();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Product product) {
            if (ModelState.IsValid)
            {
                await productRepo.AddAsync(product);
                return RedirectToAction("Index");
            }
            ViewBag.categories = await categoryRepo.GetAllAsync();
            return View(product);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();
            else if(id==0) return BadRequest();
            var product =await productRepo.GetByIdAsync(id.Value);
            if(product == null) return NotFound();
            ViewBag.categories = await categoryRepo.GetAllAsync();

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product) {
            if (ModelState.IsValid)
            {
                product.ProductId = id;
                await productRepo.EditAsync(product);
                return RedirectToAction("Index");
            }

            ViewBag.categories = await categoryRepo.GetAllAsync();

            return View(product);
        }

        public async Task<IActionResult> Delete(int? id) {
            if (id == null) return BadRequest();
            else if (id == 0) return BadRequest();
            
            if(! await productRepo.DeleteAsync(id.Value)) return NotFound();
           
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? id) {
            if (id == null) return BadRequest();
            else if (id == 0) return BadRequest();
            var product = await productRepo.GetByIdAsync(id.Value);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using GuitarShop.Models;
using Microsoft.EntityFrameworkCore;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        private ShopContext context;

        public ProductController(ShopContext ctx)
        {
            context = ctx;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult List(string id = "All")
        {
            ViewBag.Categories = context.Categories.ToList();
            ViewBag.SelectedCategory = id;

            List<Product> products = null!;
            if (id == "All")
            {
                products = context.Products.ToList();
            } 
            else
            {
                products = context.Products
                           .Where(p => p.Category.Name == id)
                           .ToList();
            }
                
            return View(products);
        }

        public IActionResult Details(int id)
        {
            Product product = context.Products
                                 .Where(p => p.ProductID == id)
                                 .Include(p => p.Category)
                                 .FirstOrDefault() ?? new Product();
            return View(product); 
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View("Update", new Product());
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            ViewBag.Categories = context.Categories.ToList();
            Product product = context.Products.Find(id) ?? new Product();
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            context.Products.Update(product);
            context.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
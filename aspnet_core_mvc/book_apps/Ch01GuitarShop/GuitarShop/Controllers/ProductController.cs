using Microsoft.AspNetCore.Mvc;
using GuitarShop.Models;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Detail(int id)
        {
            Product product = DB.GetProduct(id);
            return View(product);
        }

        public IActionResult List()
        {
            List<Product> products = DB.GetProducts();
            return View(products);
        }
    }
}

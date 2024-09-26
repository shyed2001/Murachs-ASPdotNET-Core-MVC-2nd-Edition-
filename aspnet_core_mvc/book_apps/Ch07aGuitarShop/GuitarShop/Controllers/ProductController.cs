using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View("List");     
        }

        public IActionResult List(string id = "All")
        {
            ViewBag.Category = id;
            return View();           
        }

        public IActionResult Details(string id)
        {
            ViewBag.ProductSlug = id;
            return View();
        }

    }
}
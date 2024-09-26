using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(string id)
        {
            TempData["message"] = id + " added to your cart";
            return RedirectToAction("List", "Product");
        }
    }
}

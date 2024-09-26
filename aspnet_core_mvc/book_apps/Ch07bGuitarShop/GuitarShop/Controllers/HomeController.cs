using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //ViewBag.Price = 12345.67;
            //ViewBag.DiscountPercent = .045;
            //ViewBag.Quantity = 1234;
            return View();
        }

        [Route("[action]")]
        public IActionResult About()
        {
            return View();
        }
    }
}

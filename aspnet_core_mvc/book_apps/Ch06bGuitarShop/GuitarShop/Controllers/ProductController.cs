using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return Content("Product controller, Index action");
        }

        [Route("[controller]s/{id?}")]
        public IActionResult List(string id = "All")
        {
            return Content("Product controller, List action, Category: " + id);
        }

        [Route("[controller]/{id}")]
        public IActionResult Detail(int id)
        {
            return Content("Product controller, Detail action, ID: " + id);
        }

        [NonAction]
        public string GetSlug(string s) => s.Replace(' ', '-').ToLower();
    }
}

using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return Content("Product controller, Index action");
        }

        //public IActionResult List(string id = "All")
        //{
        //    return Content("Product controller, List action, id: " + id);
        //}

        //public IActionResult List(string id, int num)
        //{
        //    return Content("Product controller, List action, " +
        //        "Category " + id + ", Page " + num);
        //}

        public IActionResult List(string id = "All", int num = 1,
            string sortby = "Price")
        {
            return Content("id=" + id + ", page=" + num + ", sortby=" + sortby);
        }

        public IActionResult Detail(int id)
        {
            return Content("Product controller, Detail action, id: " + id);
        }
    }
}

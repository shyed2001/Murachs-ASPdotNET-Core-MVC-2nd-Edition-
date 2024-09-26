using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List(string id = "All")
        {
            // convert string to object before passing to View() method. Otherwise,
            // method will think you're passing the name of the view to load
            Object objID = (Object)id;

            return View(objID);
        }

        public IActionResult Detail(int id)
        {
            return View(id);
        }
    }
}

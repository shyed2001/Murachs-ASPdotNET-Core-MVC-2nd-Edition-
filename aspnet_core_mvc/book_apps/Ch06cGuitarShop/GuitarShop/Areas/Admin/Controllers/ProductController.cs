using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("[area]/[controller]s/{id?}")]
        public IActionResult List(string id = "All")
        {
            // convert string to object before passing to View() method. Otherwise,
            // method will think you're passing the name of the view to load
            Object objID = (Object)id;

            return View(objID);
        }

        public IActionResult Add()
        {
            return View("AddUpdate");
        }

        public IActionResult Update(int id)
        {
            return View("AddUpdate", id);
        }

        public IActionResult Delete(int id)
        {
            return View(id);
        }
    }
}

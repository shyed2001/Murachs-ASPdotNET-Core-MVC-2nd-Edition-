using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ToDoContext context;
        public HomeController(ToDoContext ctx) => context = ctx;

        public ViewResult Index(string id)
        {
            // load current filters and data needed for filter drop downs in ToDoViewModel
            var model = new ToDoViewModel { 
                Filters = new Filters(id),
                Categories = context.Categories.ToList(),
                Statuses = context.Statuses.ToList(),
                DueFilters = Filters.DueFilterValues
            };

            // get open tasks from database based on current filters
            IQueryable<ToDo> query = context.ToDos
                .Include(t => t.Category).Include(t => t.Status);

            if (model.Filters.HasCategory) {
                query = query.Where(t => t.CategoryId == model.Filters.CategoryId);
            }
            if (model.Filters.HasStatus) {
                query = query.Where(t => t.StatusId == model.Filters.StatusId);
            }
            if (model.Filters.HasDue) {
                var today = DateTime.Today;
                if (model.Filters.IsPast)
                    query = query.Where(t => t.DueDate < today);
                else if (model.Filters.IsFuture)
                    query = query.Where(t => t.DueDate > today);
                else if (model.Filters.IsToday)
                    query = query.Where(t => t.DueDate == today);
            }
            model.Tasks = query.OrderBy(t => t.DueDate).ToList();

            return View(model);
        }

        [HttpGet]
        public ViewResult Add()
        {
            var model = new ToDoViewModel
            {
                Categories = context.Categories.ToList(),
                Statuses = context.Statuses.ToList(),
                CurrentTask = new ToDo { StatusId = "open" }  // set default value for drop-down
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ToDoViewModel model)
        {
            if (ModelState.IsValid)
            {
                context.ToDos.Add(model.CurrentTask);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                model.Categories = context.Categories.ToList();
                model.Statuses = context.Statuses.ToList();
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult MarkComplete([FromRoute]string id, ToDo selected)
        {
            selected = context.ToDos.Find(selected.Id)!;  // use null-forgiving operator to suppress null warning
            if (selected != null)
            {
                selected.StatusId = "closed";
                context.SaveChanges();
            }

            return RedirectToAction("Index", new { ID = id });
        }

        [HttpPost]
        public IActionResult DeleteComplete(string id)
        {
            var toDelete = context.ToDos
                .Where(t => t.StatusId == "closed").ToList();

            foreach(var task in toDelete)
            {
                context.ToDos.Remove(task);
            }
            context.SaveChanges();

            return RedirectToAction("Index", new { ID = id });
        }
    }
}
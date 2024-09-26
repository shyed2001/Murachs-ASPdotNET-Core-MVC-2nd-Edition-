using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class ClassController : Controller
    {
        private IRepository<Class> classes { get; set; }
        private IRepository<Teacher> teachers { get; set; }
        private IRepository<Day> days { get; set; }

        public ClassController(IRepository<Class> classRep,
            IRepository<Teacher> teacherRep, IRepository<Day> daysRep)
        {
            classes = classRep;
            teachers = teacherRep;
            days = daysRep;
        }

        public RedirectToActionResult Index() => RedirectToAction("Index", "Home");

        [HttpGet]
        public ViewResult Add()
        {
            this.LoadViewBag("Add");
            return View("AddEdit", new Class());
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            this.LoadViewBag("Edit");
            var c = this.GetClass(id);
            return View("AddEdit", c);
        }

        [HttpPost]
        public IActionResult Add(Class c)
        {
            bool isAdd = c.ClassId == 0;

            if (ModelState.IsValid) {
                if (isAdd)
                    classes.Insert(c);
                else
                    classes.Update(c);
                classes.Save();
                return RedirectToAction("Index", "Home");
            }
            else {
                string operation = (isAdd) ? "Add" : "Edit";
                this.LoadViewBag(operation);
                return View("AddEdit", c);
            }
        }

        [HttpGet]
        public ViewResult Delete(int id)
        {
            var c = this.GetClass(id);
            return View(c);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Class c)
        {
            classes.Delete(c);
            classes.Save();
            return RedirectToAction("Index", "Home");
        }

        // private helper methods
        private Class GetClass(int id)
        {
            var classOptions = new QueryOptions<Class> {
                Includes = "Teacher, Day",
                Where = c => c.ClassId == id
            };
            return classes.Get(classOptions) ?? new Class();
        }
        private void LoadViewBag(string operation)
        {
            ViewBag.Days = days.List(new QueryOptions<Day> {
                OrderBy = d => d.DayId
            });
            ViewBag.Teachers = teachers.List(new QueryOptions<Teacher> {
                OrderBy = t => t.LastName
            });
            ViewBag.Operation = operation;
        }
    }
}
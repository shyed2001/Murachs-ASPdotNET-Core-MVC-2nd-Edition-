using Microsoft.AspNetCore.Mvc;
using ClassSchedule.Models;

namespace ClassSchedule.Controllers
{
    public class HomeController : Controller
    {
        private Repository<Class> classes { get; set; }
        private Repository<Day> days { get; set; }

        public HomeController(ClassScheduleContext ctx)
        {
            classes = new Repository<Class>(ctx);
            days = new Repository<Day>(ctx);
        }

        public ViewResult Index(int id)
        {
            // options for Days query
            var dayOptions = new QueryOptions<Day> {
                OrderBy = d => d.DayId
            };

            // options for Classes query
            var classOptions = new QueryOptions<Class> {
                Includes = "Teacher, Day"
            };

            // order classes by day and then time on first load (ie, there's no filter value).
            // Otherwise, filter by day and order by time.
            if (id == 0) {
                classOptions.OrderBy = c => c.DayId;
                classOptions.ThenOrderBy = c => c.MilitaryTime;
            }
            else {
                classOptions.Where = c => c.DayId == id;
                classOptions.OrderBy = c => c.MilitaryTime;
            }

            // execute queries
            var dayList = days.List(dayOptions);
            var classList = classes.List(classOptions);

            // send data to view
            ViewBag.Id = id;
            ViewBag.Days = dayList;
            return View(classList);
        }
    }
}
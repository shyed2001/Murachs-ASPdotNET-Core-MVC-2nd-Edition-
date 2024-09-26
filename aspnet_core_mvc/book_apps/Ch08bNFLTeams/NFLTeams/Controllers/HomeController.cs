using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFLTeams.Models;

namespace NFLTeams.Controllers
{
    public class HomeController : Controller
    {
        private TeamContext context;

        public HomeController(TeamContext ctx)
        {
            context = ctx;
        }

        public ViewResult Index(TeamsViewModel model)
        {
            // get conferences and divisions from database 
            model.Conferences = context.Conferences.ToList();
            model.Divisions = context.Divisions.ToList();

            // get teams from database - filter by conference and division
            IQueryable<Team> query = context.Teams.OrderBy(t => t.Name);
            if (model.ActiveConf != "all")
                query = query.Where(
                    t => t.Conference.ConferenceID.ToLower() == model.ActiveConf.ToLower());
            if (model.ActiveDiv != "all")
                query = query.Where(
                    t => t.Division.DivisionID.ToLower() == model.ActiveDiv.ToLower());
            model.Teams = query.ToList();

            // pass view model to view
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(string id)
        {
            var team = context.Teams
                        .Include(t => t.Conference)
                        .Include(t => t.Division)
                        .FirstOrDefault(t => t.TeamID == id) ?? new Team();
            return View(team);
        }
    }
}
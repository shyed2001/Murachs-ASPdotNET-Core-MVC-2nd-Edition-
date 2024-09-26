using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFLTeams.Models;

namespace NFLTeams.Controllers
{
    public class FavoritesController : Controller
    {
        private TeamContext context;
        public FavoritesController(TeamContext ctx) => context = ctx;

        [HttpGet]
        public ViewResult Index()
        {
            // get favorite teams and current conference and division
            // from session, pass to view in view model
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamsViewModel
            {
                ActiveConf = session.GetActiveConf(),
                ActiveDiv = session.GetActiveDiv(),
                Teams = session.GetMyTeams(),
                UserName = session.GetName()
            };

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Add(Team team)
        {
            // get full team data from database
            team = context.Teams
                 .Include(t => t.Conference)
                 .Include(t => t.Division)
                 .Where(t => t.TeamID == team.TeamID)
                 .FirstOrDefault() ?? new Team();

            // add team to favorite teams in session
            var session = new NFLSession(HttpContext.Session);
            var teams = session.GetMyTeams();
            teams.Add(team);
            session.SetMyTeams(teams);

            // set add message
            TempData["message"] = $"{team.Name} added to your favorites";

            // redirect to Home page
            return RedirectToAction("Index", "Home", 
                new
                {
                    ActiveConf = session.GetActiveConf(),
                    ActiveDiv = session.GetActiveDiv()
                });
        }

        [HttpPost]
        public RedirectToActionResult Delete()
        {
            // delete favorite teams from session
            var session = new NFLSession(HttpContext.Session);
            session.RemoveMyTeams();

            // set delete message
            TempData["message"] = "Favorite teams cleared";

            // redirect to Home page
            return RedirectToAction("Index", "Home",
                new {
                    ActiveConf = session.GetActiveConf(),
                    ActiveDiv = session.GetActiveDiv()
                });
        }
    }
}
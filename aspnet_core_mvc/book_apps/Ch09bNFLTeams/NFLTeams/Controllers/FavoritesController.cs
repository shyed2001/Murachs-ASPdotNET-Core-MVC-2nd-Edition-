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
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamsViewModel
            {
                ActiveConf = session.GetActiveConf(),
                ActiveDiv = session.GetActiveDiv(),
                Teams = session.GetMyTeams()
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

            // add team to favorite teams in session and cookie
            var session = new NFLSession(HttpContext.Session);
            var cookies = new NFLCookies(Response.Cookies);

            var teams = session.GetMyTeams();
            teams.Add(team);
            session.SetMyTeams(teams);        
            cookies.SetMyTeamIds(teams);

            // set add message
            TempData["message"] = $"{team.Name} added to your favorites";

            // redirect to Home page
            return RedirectToAction("Index", "Home", 
                new {
                    ActiveConf = session.GetActiveConf(),
                    ActiveDiv = session.GetActiveDiv()
                });
        }

        [HttpPost]
        public RedirectToActionResult Delete()
        {
            // delete favorite teams from session and cookie
            var session = new NFLSession(HttpContext.Session);
            var cookies = new NFLCookies(Response.Cookies);

            session.RemoveMyTeams();
            cookies.RemoveMyTeamIds();

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
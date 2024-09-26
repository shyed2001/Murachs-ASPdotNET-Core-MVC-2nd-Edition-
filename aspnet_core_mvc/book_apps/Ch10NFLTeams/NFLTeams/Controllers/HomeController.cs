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
            // store active conference and division in session
            var session = new NFLSession(HttpContext.Session);
            session.SetActiveConf(model.ActiveConf);
            session.SetActiveDiv(model.ActiveDiv);

            // if no count value in session, use data in cookie
            // to restore fave teams in session 
            int? count = session.GetMyTeamCount();
            if (!count.HasValue)
            {
                var cookies = new NFLCookies(Request.Cookies);
                string[] ids = cookies.GetMyTeamIds();

                if (ids.Length > 0)
                {
                    var myteams = context.Teams
                        .Include(t => t.Conference)
                        .Include(t => t.Division)
                        .Where(t => ids.Contains(t.TeamID))
                        .ToList();
                    session.SetMyTeams(myteams);
                }
            }

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

        public IActionResult Details(string id)
        {
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamsViewModel
            {
                Team = context.Teams
                    .Include(t => t.Conference)
                    .Include(t => t.Division)
                    .FirstOrDefault(t => t.TeamID == id) ?? new Team(),
                ActiveDiv = session.GetActiveDiv(),
                ActiveConf = session.GetActiveConf()
            };
            return View(model);
        }

    }
}
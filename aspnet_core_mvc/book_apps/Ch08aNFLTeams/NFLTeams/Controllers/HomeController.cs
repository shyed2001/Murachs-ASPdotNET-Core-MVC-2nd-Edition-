using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        public ViewResult Index(string activeConf = "all", string activeDiv = "all")
        {
            // store selected conference and division IDs in view bag
            ViewBag.ActiveConf = activeConf;
            ViewBag.ActiveDiv = activeDiv;

            // store conferences and divisions from database in view bag
            ViewBag.Conferences = context.Conferences.ToList();
            ViewBag.Divisions = context.Divisions.ToList();

            // get teams - filter by conference and division
            IQueryable<Team> query = context.Teams.OrderBy(t => t.Name);
            if (activeConf != "all")
                query = query.Where(
                    t => t.Conference.ConferenceID.ToLower() == activeConf.ToLower());
            if (activeDiv != "all")
                query = query.Where(
                    t => t.Division.DivisionID.ToLower() == activeDiv.ToLower());

            // pass list of teams to view as model
            var teams = query.ToList();
            return View(teams);
        }
    }
}
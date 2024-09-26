﻿namespace NFLTeams.Models
{
    public class TeamsViewModel
    {
        public string ActiveConf { get; set; } = "all";
        public string ActiveDiv { get; set; } = "all";
        public Team Team { get; set; } = new Team();
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<Conference> Conferences { get; set; } = new List<Conference>();
        public List<Division> Divisions { get; set; } = new List<Division>();

        // methods to help view determine active link
        public string CheckActiveConf(string c) =>
            c.ToLower() == ActiveConf.ToLower() ? "active" : "";

        public string CheckActiveDiv(string d) =>
            d.ToLower() == ActiveDiv.ToLower() ? "active" : "";

    }
}

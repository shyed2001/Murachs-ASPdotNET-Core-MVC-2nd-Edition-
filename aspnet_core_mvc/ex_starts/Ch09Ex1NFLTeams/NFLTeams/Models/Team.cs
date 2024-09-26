using System.Text.Json.Serialization;

namespace NFLTeams.Models
{
    public class Team
    {
        public string TeamID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Conference Conference { get; set; } = null!;
        public Division Division { get; set; } = null!;
        public string LogoImage { get; set; } = string.Empty;
    }

    public class User
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        [JsonIgnore]
        public string FullName => FirstName + " " + LastName;
    }
}

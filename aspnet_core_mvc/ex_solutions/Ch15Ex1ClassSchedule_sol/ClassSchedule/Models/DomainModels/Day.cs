using System.ComponentModel.DataAnnotations;

namespace ClassSchedule.Models
{
    public class Day
    {
        public Day() => Classes = new HashSet<Class>();    // initialize navigation property

        public int DayId { get; set; }                     // Primary key

        // no error messages included bc users won't be entering - this is so 
        // EF will generate a non-null nvarchar with length shorter than max
        [StringLength(10)]  
        [Required()]
        public string Name { get; set; } = string.Empty;

        public ICollection<Class> Classes { get; set; }    // Navigation property
    }
}

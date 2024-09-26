using System.ComponentModel.DataAnnotations;

namespace ClassSchedule.Models
{
    public class Teacher
    {
        public Teacher() => Classes = new HashSet<Class>();   // initialize navigation property

        public int TeacherId { get; set; }                    // Primary key

        [Display(Name = "First Name")]
        [StringLength(100, ErrorMessage = "First name may not exceed 100 characters.")]
        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; } = string.Empty;

        [Display(Name = "Last Name")]
        [StringLength(100, ErrorMessage = "Last name may not exceed 100 characters.")]
        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; } = string.Empty;

        public ICollection<Class> Classes { get; set; }       // Navigation property

        // read-only display property
        public string FullName => $"{FirstName} {LastName}";
    }
}

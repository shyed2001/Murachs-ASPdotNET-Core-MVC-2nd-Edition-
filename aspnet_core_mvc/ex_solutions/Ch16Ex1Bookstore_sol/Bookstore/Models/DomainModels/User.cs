using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;  // for NotMapped

namespace Bookstore.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Please enter a first name.")]
        [StringLength(255)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a last name.")]
        [StringLength(255)]
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public IList<string> RoleNames { get; set; } = null!;
    }
}

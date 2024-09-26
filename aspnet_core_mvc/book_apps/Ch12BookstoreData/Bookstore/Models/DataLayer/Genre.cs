using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models.DataLayer
{
    public partial class Genre
    {
        public Genre()
        {
            Books = new HashSet<Book>();
        }

        [Key]
        [StringLength(10)]
        public string GenreId { get; set; } = null!;
        [StringLength(25)]
        public string Name { get; set; } = null!;

        [InverseProperty("Genre")]
        public virtual ICollection<Book> Books { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class BookstoreContext : DbContext
    {
        public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
        { }

        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Author> Authors { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure entities
            modelBuilder.ApplyConfiguration(new ConfigureGenres());
            modelBuilder.ApplyConfiguration(new ConfigureBooks());
            modelBuilder.ApplyConfiguration(new ConfigureAuthors());
            modelBuilder.ApplyConfiguration(new ConfigureBookAuthors());
        }
    }
}

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
            /* configure by convention - use default names for columns in linking table */
            //modelBuilder.Entity<Book>()
            //    .HasMany(b => b.Authors)
            //    .WithMany(a => a.Books)
            //    .UsingEntity(ba => ba.HasData(
            //        new { BooksBookId = 1, AuthorsAuthorId = 18 },
            //        new { BooksBookId = 2, AuthorsAuthorId = 20 },
            //        //
            //        new { BooksBookId = 29, AuthorsAuthorId = 25 })
            //);

            /* configure with attributes - use column names set in ForeignKey attributes */
            //modelBuilder.Entity<Book>()
            //    .HasMany(b => b.Authors)
            //    .WithMany(a => a.Books)
            //    .UsingEntity(ba => ba.HasData(
            //        new { BookId = 1, AuthorId = 18 },
            //        new { BookId = 2, AuthorId = 20 },
            //        //
            //        new { BookId = 29, AuthorId = 25 })
            //);


            /* configure with Fluent API */
            //modelBuilder.Entity<Book>()
            //    .HasMany(b => b.Authors)        // define relationship
            //    .WithMany(a => a.Books)
            //    .UsingEntity<Dictionary<string, object>>(
            //        "BookAuthors",              // set linking table name
            //        ba => ba.HasOne<Author>()   // set foreign key columns
            //            .WithMany()
            //            .HasForeignKey("AuthorId"),
            //        ba => ba.HasOne<Book>()
            //            .WithMany()
            //            .HasForeignKey("BookId"),
            //        ba => ba                                               // seed initial data
            //            .HasData(
            //               new { BookId = 1, AuthorId = 18 },
            //               new { BookId = 2, AuthorId = 20 },
            //               //
            //               new { BookId = 29, AuthorId = 25 })
            //);

            // configure entities
            modelBuilder.ApplyConfiguration(new ConfigureGenres());
            modelBuilder.ApplyConfiguration(new ConfigureBooks());
            modelBuilder.ApplyConfiguration(new ConfigureAuthors());
            modelBuilder.ApplyConfiguration(new ConfigureBookAuthors());
        }
    }
}

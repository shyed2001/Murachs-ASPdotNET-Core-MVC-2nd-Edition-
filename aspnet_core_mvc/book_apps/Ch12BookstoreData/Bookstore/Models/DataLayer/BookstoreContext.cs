using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bookstore.Models.DataLayer
{
    //public partial class Bookstore_testContext : DbContext
    //{
    //    public Bookstore_testContext()
    //    {
    //    }

    //    public Bookstore_testContext(DbContextOptions<Bookstore_testContext> options)
    //        : base(options)
    //    {
    //    }

    //    public virtual DbSet<Author> Authors { get; set; } = null!;
    //    public virtual DbSet<Book> Books { get; set; } = null!;
    //    public virtual DbSet<Genre> Genres { get; set; } = null!;

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        if (!optionsBuilder.IsConfigured)
    //        {
    //            optionsBuilder.UseSqlServer("name=BookstoreContext");
    //        }
    //    }

    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Author>(entity =>
    //        {
    //            entity.HasMany(d => d.Books)
    //                .WithMany(p => p.Authors)
    //                .UsingEntity<Dictionary<string, object>>(
    //                    "BookAuthor",
    //                    l => l.HasOne<Book>().WithMany().HasForeignKey("BookId"),
    //                    r => r.HasOne<Author>().WithMany().HasForeignKey("AuthorId"),
    //                    j =>
    //                    {
    //                        j.HasKey("AuthorId", "BookId");

    //                        j.ToTable("BookAuthors");

    //                        j.HasIndex(new[] { "BookId" }, "IX_BookAuthors_BookId");
    //                    });
    //        });

    //        modelBuilder.Entity<Book>(entity =>
    //        {
    //            entity.HasOne(d => d.Genre)
    //                .WithMany(p => p.Books)
    //                .HasForeignKey(d => d.GenreId)
    //                .OnDelete(DeleteBehavior.ClientSetNull);
    //        });

    //        OnModelCreatingPartial(modelBuilder);
    //    }

    //    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    //}
}

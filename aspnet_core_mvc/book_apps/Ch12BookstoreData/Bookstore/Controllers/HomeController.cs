using Bookstore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Controllers
{
    public class HomeController : Controller
    {
        private BookstoreContext context { get; set; }
        public HomeController(BookstoreContext ctx) => context = ctx;

        public IActionResult Index(string id = "")
        {
            if (id.ToLower() == "test")
            {
                // test concurrency
                var book = context.Books.Find(1)!;             // get a book from the database 

                try
                {
                    book.Price = 14.99;                        // change price in memory

                    context.Database.ExecuteSqlRaw(            // change price in the database
                        "UPDATE dbo.Books SET Price = Price + 0.01 WHERE BookId = 1");

                    context.SaveChanges();
                }
                catch(DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var dbValues = entry.GetDatabaseValues();
                    if (dbValues == null)
                    {
                        ModelState.AddModelError("", "Unable to save - "
                            + "this book was deleted by another user.");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save - "
                            + "this book was modified by another user. "
                            + "The current database values are displayed "
                            + "below.");

                        var dbBook = (Book)dbValues.ToObject();

                        if (dbBook.Title != book.Title)
                            ModelState.AddModelError("",
                                $"Current db title: {dbBook.Title}");

                        if (dbBook.Price != book.Price)
                            ModelState.AddModelError("",
                                $"Current db price: {dbBook.Price.ToString("c")}");
                    }
                }
            }

            var genres = context.Genres
                .Include(g => g.Books)
                .ThenInclude(b => b.Authors)
                .ToList();

            var genres2 = context.Genres
                .Include("Books.Authors")
                .ToList();

            var books = context.Books
                .Include("Genre")
                .Include("Authors")
                .Where(b => b.Title.Contains("Pride"))
                .ToList();
            return View(books);
        }
    }
}
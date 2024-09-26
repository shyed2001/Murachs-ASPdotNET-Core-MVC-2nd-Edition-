using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Components
{
    public class BookDetailsLink : ViewComponent
    {
        public IViewComponentResult Invoke(int id, string title)
        {
            Book book = new Book { 
                BookId = id,
                Title = title
            };
            return View("~/Views/Shared/_BookLinkPartial.cshtml", book);
        }
    }
}

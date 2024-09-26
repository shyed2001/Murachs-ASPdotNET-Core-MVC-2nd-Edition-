using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Components
{
    public class AuthorDetailsLink : ViewComponent
    {
        public IViewComponentResult Invoke(int id, string name)
        {
            Author author = new Author { 
                AuthorId = id,
                FirstName = name
            };
            return View("~/Views/Shared/_AuthorLinkPartial.cshtml", author);
        }
    }
}

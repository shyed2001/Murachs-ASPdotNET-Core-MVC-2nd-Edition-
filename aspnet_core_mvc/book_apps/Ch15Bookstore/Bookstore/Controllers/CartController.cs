using Microsoft.AspNetCore.Mvc;
using Bookstore.Models;

namespace Bookstore.Controllers
{
    public class CartController : Controller
    {
        private IRepository<Book> data { get; set; }
        private ICart cart { get; set; }

        public CartController(IRepository<Book> rep, ICart c) 
        { 
            data = rep;
            cart = c;
            cart.Load(data);
        }

        public ViewResult Index() 
        {
            // create a new view model object with cart info and pass it to the view
            var vm = new CartViewModel
            {
                List = cart.List,
                Subtotal = cart.Subtotal
            };

            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            // get the book the user chose from the database
            var book = data.Get(new QueryOptions<Book> {
                Where = b => b.BookId == id,
                Includes = "Authors, Genre"
            });

            if (book == null){  // book not in database
                TempData["message"] = "Unable to add book to cart.";   
            }
            else { // create a new CartItem object with a default quantity of one.
                CartItem item = new CartItem {
                    Book = new BookDTO(book),
                    Quantity = 1  // default value
                };

                // add new item to cart and save to session state
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{book.Title} added to cart";
            }

            return RedirectToAction("List", "Book");
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            CartItem? item = cart.GetById(id);
            if (item != null)
            {
                cart.Remove(item);
                cart.Save();
                TempData["message"] = $"{item.Book.Title} removed from cart.";
            }
            return RedirectToAction("Index");
        }
                
        [HttpPost]
        public RedirectToActionResult Clear()
        {
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //get selected cart item from session and pass it to the view
            CartItem? item = cart.GetById(id);
            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            cart.Edit(item);
            cart.Save();

            TempData["message"] = $"{item.Book.Title} updated";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}
using Microsoft.AspNetCore.Mvc.ViewFeatures;    // for ITempDataDictionary
using Bookstore.Areas.Admin.Controllers;

namespace Bookstore.Tests
{
    public class AdminBookControllerTests
    {
        public BookController GetBookController()
        {
            // mock book repository
            var bookRep = new Mock<IBookRepository>();
            bookRep.Setup(m => m.Get(It.IsAny<QueryOptions<Book>>()))
                .Returns(new Book());
            bookRep.Setup(m => m.List(It.IsAny<QueryOptions<Book>>()))
               .Returns(new List<Book>());

            // mock author repository
            var authorRep = new Mock<IRepository<Author>>();
            authorRep.Setup(m => m.List(It.IsAny<QueryOptions<Author>>()))
                .Returns(new List<Author>());

            // mock genre repository
            var genreRep = new Mock<IRepository<Genre>>();
            genreRep.Setup(m => m.List(It.IsAny<QueryOptions<Genre>>()))
                .Returns(new List<Genre>());

            // return controller
            return new BookController(
                bookRep.Object, authorRep.Object, genreRep.Object);
        }

        [Fact]
        public void Edit_GET_ModelIsBookViewModel()
        {
            // arrange
            var controller = GetBookController();

            // act
            var model = controller.Edit(1).ViewData.Model;

            // assert
            Assert.IsType<BookViewModel>(model);
        }

        [Fact]
        public void Edit_POST_ReturnsViewResultIfModelIsNotValid()
        {
            // arrange
            var controller = GetBookController();

            controller.ModelState.AddModelError("", "Test error message.");
            BookViewModel vm = new BookViewModel();

            // act
            var result = controller.Edit(vm);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Edit_POST_ReturnsRedirectToActionResultIfModelIsValid()
        {
            // arrange
            var controller = GetBookController();

            var temp = new Mock<ITempDataDictionary>();
            controller.TempData = temp.Object;
            BookViewModel vm = new BookViewModel { Book = new Book() };

            // act
            var result = controller.Edit(vm);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
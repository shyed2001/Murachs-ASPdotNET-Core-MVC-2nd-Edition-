using Bookstore.Controllers;

namespace Bookstore.Tests
{
    public class BookControllerTests
    {
        [Fact]
        public void Index_ReturnsARedirectToActionResult()
        {
            // arrange
            var rep = new Mock<IRepository<Book>>();
            var controller = new BookController(rep.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Index_RedirectsToListActionMethod()
        {
            // arrange
            var rep = new Mock<IRepository<Book>>();
            var controller = new BookController(rep.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.Equal("List", result.ActionName);
        }

        [Fact]
        public void Details_ModelIsABookObject()
        {
            // arrange
            var rep = new Mock<IRepository<Book>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Book>>()))
                .Returns(new Book());

            var controller = new BookController(rep.Object);

            // act
            var model = controller.Details(1).ViewData.Model;

            // assert
            Assert.IsType<Book>(model);
        }

    }
}
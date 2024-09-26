using Bookstore.Controllers;

namespace Bookstore.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsViewResult()
        {
            // FakeBookRepository
            // arrange
            /*
            var rep = new FakeBookRepository();
            var controller = new HomeController(rep);
            */

            // Moq
            // arrange
            var rep = new Mock<IRepository<Book>>();
            rep.Setup(m => m.Get(It.IsAny<QueryOptions<Book>>()))
                .Returns(new Book());
            var controller = new HomeController(rep.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexActionMethod_ModelIsABookObject()
        {
            // arrange
            var rep = new FakeBookRepository();
            var controller = new HomeController(rep);

            // act
            var model = controller.Index().ViewData.Model as Book;

            // assert
            Assert.IsType<Book>(model);
        }
    }
}
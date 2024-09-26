using ClassSchedule.Controllers;

namespace ClassScheduleTests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsAViewResult()
        {
            // arrange
            var classRep = new Mock<IRepository<Class>>();
            var daysRep = new Mock<IRepository<Day>>();
            var controller = new HomeController(classRep.Object, daysRep.Object);

            // act
            var result = controller.Index(0);

            // assert
            Assert.IsType<ViewResult>(result);
        }
    }
}

using ClassSchedule.Controllers;

namespace ClassScheduleTests
{
    public class TeacherControllerTests
    {
        [Fact]
        public void IndexActionMethod_ReturnsAViewResult()
        {
            // arrange
            var rep = new Mock<IRepository<Teacher>>();
            var controller = new TeacherController(rep.Object);

            // act
            var result = controller.Index();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void IndexActionMethod_ModelIsAListOfTeacherObjects()
        {
            // arrange
            var rep = new Mock<IRepository<Teacher>>();
            rep.Setup(m => m.List(It.IsAny<QueryOptions<Teacher>>()))
                .Returns(new List<Teacher>());
            var controller = new TeacherController(rep.Object);

            // act
            var model = controller.Index().ViewData.Model;

            // assert
            Assert.IsType<List<Teacher>>(model);
        }
    }
}
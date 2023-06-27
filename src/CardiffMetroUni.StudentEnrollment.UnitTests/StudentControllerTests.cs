using CardiffMetroUni.StudentEnrollment.Infrastructure.Data;
using CardiffMetroUni.StudentEnrollment.WebAPI.Controllers;
using System.Net;

namespace CardiffMetroUni.StudentEnrollment.UnitTests
{
    [TestFixture]
    public class StudentControllerTests
    {


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetAllStudents_ReturnsOkResultWithStudentsModel()
        {
            // Arrange
            var expectedStatusCode = HttpStatusCode.OK;
            var httpClient = new HttpClient();

            // Act
            var response = await httpClient.GetAsync("http://localhost:7011/api/GetStudents");
            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.AreEqual(expectedStatusCode, response.StatusCode);
            Assert.That(result, Is.Not.Null);
            // Add further assertions for the expected values in the response

            // Dispose the HttpClient
            httpClient.Dispose();
        }
    }
}
using Capstone.DataAccess;
using Capstone.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTest
{
	public class UnitTest
	{
		[Fact]
		public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
		{
			// Arrange
			var mockRepo = new Mock<IUnitOfWork>();
			mockRepo.Setup(repo => repo.Product.GetAll());
			var controller = new HomeController(mockRepo.Object);

			// Act
			var result = await controller.Index();

			// Assert
			var viewResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
				viewResult.ViewData.Model);
			Assert.Equal(2, model.Count());
		}
	}
}
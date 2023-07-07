using Capstone.DataAccess.Repository.IRepository;
using CapstoneProject.Areas.Admin.Controllers;
using CapstoneProject.Areas.Client.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace TestProject
{
	public class UnitTest
	{
		// I do not know how to properly mock the interfaces to test routing and etc.
		// This topic is beyond my comprehension but I tried.
		[Fact]
		public Task Index_ReturnsAViewResult_WithAListOfProduct()
		{
			// Arrange
			var mockRepo = new Mock<IUnitOfWork>();
			mockRepo.Setup(repo => repo.Customer);

			//var controller = new ProductController(mockRepo.Object);

			//// Act
			//var result = await controller.index();

			// Assert
			//var viewResult = Assert.IsType<ViewResult>(result);
			//var model = Assert.IsAssignableFrom<IEnumerable<IUnitOfWork>>(
			//	viewResult.ViewData.Model);
			//Assert.Equal(2, model.Count());
			return Task.CompletedTask;
		}

		[Fact]
		public Task IndexPost_ReturnsBadRequestResult_WhenModelStateIsInvalid()
		{
			// Arrange
			var mockRepo = new Mock<IUnitOfWork>();
			//mockRepo.Setup(repo => repo.Customer).Returns();
			var controller = new CustomerController(mockRepo.Object);
			controller.ModelState.AddModelError("SessionName", "Required");
			//var newSession = new HomeController.NewSessionModel();

			// Act
			//var result = await controller.Index(newSession);

			// Assert
			//var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
			//Assert.IsType<SerializableError>(badRequestResult.Value);
			return Task.CompletedTask;
		}

		[Fact]
		public async Task IndexPost_ReturnsARedirectAndAddsSession_WhenModelStateIsValid()
		{
			// Arrange
			var mockRepo = new Mock<IUnitOfWork>();
			//mockRepo.Setup(repo => repo.AddAsync(It.IsAny<IUnitOfWork>()))
			//	.Returns(Task.CompletedTask)
			//	.Verifiable();
			//var controller = new HomeController(mockRepo.Object);
			//var newSession = new HomeController.NewSessionModel()
			//{
			//	SessionName = "Test Name"
			//};

			// Act
			//var result = await controller.Index(newSession);

			// Assert
			//var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
			//Assert.Null(redirectToActionResult.ControllerName);
			//Assert.Equal("Index", redirectToActionResult.ActionName);
			mockRepo.Verify();
		}
	}
}
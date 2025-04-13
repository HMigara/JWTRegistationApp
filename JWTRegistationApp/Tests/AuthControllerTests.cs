/*// Tests/AuthControllerTests.cs
using JWTRegistationApp.Controllers;
using JWTRegistationApp.Models;

using JWTRegistationApp.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Authentication.Tests
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Register_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(service => service.UserExists(It.IsAny<string>()))
                          .ReturnsAsync(false);
            mockAuthService.Setup(service => service.RegisterUser(It.IsAny<RegisterModel>()))
                          .ReturnsAsync(true);

            var controller = new AuthController(mockAuthService.Object);
            var model = new RegisterModel
            {
                Username = "testuser",
                Password = "password123",
                RegistrationDate = DateTime.Now
                // IFormFile will be mocked separately if needed
            };

            // Act
            var result = await controller.Register(model);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_WithExistingUsername_ReturnsBadRequest()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(service => service.UserExists(It.IsAny<string>()))
                          .ReturnsAsync(true);

            var controller = new AuthController(mockAuthService.Object);
            var model = new RegisterModel
            {
                Username = "existinguser",
                Password = "password123",
                RegistrationDate = DateTime.Now
            };

            // Act
            var result = await controller.Register(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsOkResultWithToken()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(service => service.Authenticate(It.IsAny<LoginModel>()))
                          .ReturnsAsync("valid_token");

            var controller = new AuthController(mockAuthService.Object);
            var model = new LoginModel
            {
                Username = "testuser",
                Password = "password123"
            };

            // Act
            var result = await controller.Login(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var resultValue = okResult.Value as dynamic;
            Assert.NotNull(resultValue);
            Assert.Equal("valid_token", resultValue.Token);
        }

        [Fact]
        public async Task Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            // Arrange
            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(service => service.Authenticate(It.IsAny<LoginModel>()))
                          .ReturnsAsync(string.Empty);

            var controller = new AuthController(mockAuthService.Object);
            var model = new LoginModel
            {
                Username = "wronguser",
                Password = "wrongpassword"
            };

            // Act
            var result = await controller.Login(model);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
        }
    }
}*/
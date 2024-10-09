using Microsoft.AspNetCore.Mvc;
using Moq;
using SistemaDeTarefas.Controllers;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;
using System.Formats.Asn1;

namespace SistemasDeTarefasTests.ControllerTests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _userController = new UserController(_mockUserService.Object);
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOkResult_WithListOfUsers()
        {
            //Arrange
            var usersList = new List<UserModel>
            {
                new UserModel { Id = 1, Name = "John Doe" },
                new UserModel { Id = 2, Name = "Jane Doe" }
            };

            _mockUserService.Setup(repo => repo.GetAll()).ReturnsAsync(usersList);

            //Act
            var result = await _userController.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<UserModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

        }

        [Fact]
        public async Task GetUserById_ReturnOkResult_WithUser()
        {
            //Arrange
            var user = new UserModel { Id = 1, Name = "John Doe" };
            _mockUserService.Setup(repo => repo.GetById(user.Id)).ReturnsAsync(user);

            //Act
            var result = await _userController.GetById(user.Id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserModel>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("John Doe", returnValue.Name);

        }

        [Fact]
        public async Task GetUserById_ReturnNotFound_WhenUserDoesNotExist()
        {
            //Arrange
            _mockUserService.Setup(repo => repo.GetById(1)).ReturnsAsync((UserModel)null);

            //Act
            var result = await _userController.GetById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task PostUser_ReturnsOkResult_WithCreatedUser()
        {
            //Arrange
            var newUser = new UserModel { Name = "New User" };
            _mockUserService.Setup(repo => repo.Post(newUser)).ReturnsAsync(newUser);

            //Act
            var result = await _userController.Post(newUser);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserModel>(okResult.Value);
            Assert.Equal("New User", returnValue.Name);

        }

        [Fact]
        public async Task PutUser_ReturnsOkResult_WithUpdatedUser()
        {
            //Arrange
            var uptadeUser = new UserModel { Id = 1, Name = "Updated User" };
            _mockUserService.Setup(repo => repo.Put(uptadeUser, 1)).ReturnsAsync(uptadeUser);

            //Act
            var result = await _userController.Put(uptadeUser, 1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<UserModel>(okResult.Value);
            Assert.Equal("Updated User", returnValue.Name);

        }

        [Fact]
        public async Task DeleteUser_ReturnOkResult_WhenUserDeleted()
        {
            // Arrange
            var userId = 1;

            _mockUserService.Setup(repo => repo.GetById(userId)).ReturnsAsync(new UserModel { Id = userId });
            _mockUserService.Setup(repo => repo.Delete(userId)).ReturnsAsync(true);

            // Act
            var result = await _userController.Delete(userId);

            // Assert
            Assert.IsType<OkResult>(result.Result);

        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFound_WhenUserNotFound()
        {
            //Arrange
            _mockUserService.Setup(repo => repo.Delete(1)).ReturnsAsync(false);

            //Act
            var result = await _userController.Delete(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

    }
}
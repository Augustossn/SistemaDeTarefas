using Microsoft.AspNetCore.Mvc;
using Moq;
using SistemaDeTarefas.Controllers;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositorios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemasDeTarefasTests.ControllerTests
{
    public class TarefaControllerTests
    {
        private readonly Mock<ITarefaService> _mockTarefaService;
        private readonly TarefaController _tarefaController;

        public TarefaControllerTests()
        {
            _mockTarefaService = new Mock<ITarefaService>();
            _tarefaController = new TarefaController(_mockTarefaService.Object);
        }

        [Fact]
        public async Task GetAllTarefa_ReturnsOkResult_WithListOfUsers()
        {
            //Arrange
            var tarefaList = new List<TarefaModel>
            {
                new TarefaModel { Id = 1, Name = "Dishes", Description = "Clean the dishes" },
                new TarefaModel { Id = 2, Name = "House", Description = "Clean the house" }
            };

            _mockTarefaService.Setup(repo => repo.GetAll()).ReturnsAsync(tarefaList);

            //Act
            var result = await _tarefaController.GetAll();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<TarefaModel>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);

        }

        [Fact]
        public async Task GetTarefaById_ReturnOkResult_WithUser()
        {
            //Arrange
            var tarefa = new TarefaModel { Id = 1, Name = "Dishes", Description = "Clean the dishes" };
            _mockTarefaService.Setup(repo => repo.GetById(tarefa.Id)).ReturnsAsync(tarefa);

            //Act
            var result = await _tarefaController.GetById(tarefa.Id);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TarefaModel>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
            Assert.Equal("Dishes", returnValue.Name);

        }

        [Fact]
        public async Task GetTarefaById_ReturnNotFound_WhenUserDoesNotExist()
        {
            //Arrange
            _mockTarefaService.Setup(repo => repo.GetById(1)).ReturnsAsync((TarefaModel)null);

            //Act
            var result = await _tarefaController.GetById(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task PostTarefa_ReturnsOkResult_WithCreatedUser()
        {
            //Arrange
            var newTarefa = new TarefaModel { Name = "New chore", Description = "Description of the chore" };
            _mockTarefaService.Setup(repo => repo.Post(newTarefa)).ReturnsAsync(newTarefa);

            //Act
            var result = await _tarefaController.Post(newTarefa);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TarefaModel>(okResult.Value);
            Assert.Equal("New chore", returnValue.Name);

        }

        [Fact]
        public async Task PutTarefa_ReturnsOkResult_WithUpdatedUser()
        {
            //Arrange
            var uptadeTarefa = new TarefaModel { Id = 1, Name = "Updated Chore", Description = "Updated description" };
            _mockTarefaService.Setup(repo => repo.Put(uptadeTarefa, 1)).ReturnsAsync(uptadeTarefa);

            //Act
            var result = await _tarefaController.Put(uptadeTarefa, 1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<TarefaModel>(okResult.Value);
            Assert.Equal("Updated Chore", returnValue.Name);

        }

        [Fact]
        public async Task DeleteTarefa_ReturnOkResult_WhenUserDeleted()
        {
            // Arrange
            var tarefaId = 1;

            _mockTarefaService.Setup(repo => repo.GetById(tarefaId)).ReturnsAsync(new TarefaModel { Id = tarefaId });
            _mockTarefaService.Setup(repo => repo.Delete(tarefaId)).ReturnsAsync(true);

            // Act
            var result = await _tarefaController.Delete(tarefaId);

            // Assert
            Assert.IsType<OkResult>(result.Result);

        }

        [Fact]
        public async Task DeleteTarefa_ReturnsNotFound_WhenUserNotFound()
        {
            //Arrange
            _mockTarefaService.Setup(repo => repo.Delete(1)).ReturnsAsync(false);

            //Act
            var result = await _tarefaController.Delete(1);

            //Assert
            Assert.IsType<NotFoundResult>(result.Result);

        }

    }
}
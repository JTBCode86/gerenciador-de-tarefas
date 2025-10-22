// Localização: Features/Tasks/Commands/CreateTask/CreateTaskCommandHandlerTests.cs
using Moq;
using System.Threading.Tasks;
using Xunit;
using TaskPilot.Domain.Interfaces;
using TaskPilot.Application.Interfaces;
using TaskPilot.Application.Features.Tasks.Commands.CreateTask;
using DomainTask = TaskPilot.Domain.Entities.Task;

namespace TaskPilot.Application.Tests.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandlerTests
    {
        [Fact]
        public async System.Threading.Tasks.Task Handle_ShouldCallRepositoryAddAsyncAndReturnNewId_WhenValidCommand()
        {
            // ARRANGE
            var mockRepo = new Mock<ITaskRepository>();
            var mockExternalService = new Mock<IExternalApiService>();

            // Setup: Simular retorno do ID 10
            mockRepo.Setup(r => r.AddAsync(It.IsAny<DomainTask>()))
                    .ReturnsAsync(10);

            // Injeção de DUAS dependências
            var handler = new CreateTaskCommandHandler(
                mockRepo.Object,
                mockExternalService.Object
            );

            var command = new CreateTaskCommand { UserId = 1, Title = "Nova Tarefa Teste" };

            // ACT
            var resultId = await handler.Handle(command);

            // ASSERT
            mockRepo.Verify(r => r.AddAsync(It.IsAny<DomainTask>()), Times.Once);
            Assert.Equal(10, resultId);
        }
    }
}

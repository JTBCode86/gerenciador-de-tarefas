// Localização: Features/Tasks/Commands/CompleteTask/CompleteTaskCommandHandlerTests.cs
using Moq;
using System.Threading.Tasks;
using Xunit;
using TaskPilot.Domain.Interfaces;
using TaskPilot.Domain.Exceptions; // Namespace da exceção
using TaskPilot.Application.Features.Tasks.Commands.CompleteTask;
using TaskPilot.Domain.Entities;
using DomainTask = TaskPilot.Domain.Entities.Task;

namespace TaskPilot.Application.Tests.Features.Tasks.Commands.CompleteTask
{
    public class CompleteTaskCommandHandlerTests
    {
        private const int CompletedStatusId = 2;

        [Fact]
        public async System.Threading.Tasks.Task Handle_ShouldThrowNotFoundException_WhenTaskDoesNotExist()
        {
            // ARRANGE
            var mockRepo = new Mock<ITaskRepository>();

            // Setup: GetByIdAsync deve retornar NULL
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((DomainTask)null);

            var handler = new CompleteTaskCommandHandler(mockRepo.Object);
            var command = new CompleteTaskCommand { TaskId = 999 };

            // ACT & ASSERT
            await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command));
        }

        // ... (Omitido o teste de sucesso para brevidade, mas deve existir)
    }
}
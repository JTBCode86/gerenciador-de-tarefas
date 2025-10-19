// Localização: Features/Tasks/Queries/GetTaskSummary/GetTaskSummaryQueryHandlerTests.cs

using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq; // Para o .ToList()
using Xunit;
using TaskPilot.Domain.Interfaces;
using TaskPilot.Domain.Entities;
using TaskPilot.Application.Features.Tasks.Queries.GetTaskSummary;
using DomainTask = TaskPilot.Domain.Entities.Task;

namespace TaskPilot.Application.Tests.Features.Tasks.Queries.GetTaskSummary
{
    // ... (Assumindo que TaskSummaryDto existe no namespace Application)

    public class GetTaskSummaryQueryHandlerTests
    {
        [Fact]
        public async System.Threading.Tasks.Task Handle_ShouldCallRepositoryAndReturnSummaryList()
        {
            // ARRANGE
            var testUserId = 1;
            var mockRepo = new Mock<ITaskRepository>();

            // Simula a lista de Entidades (Task)
            var tasksFromRepo = new List<DomainTask>
            {
                new DomainTask { Id = 1, Title = "Daily Standup", StatusId = 1, UserId = testUserId },
                new DomainTask { Id = 2, Title = "Bug Fix", StatusId = 2, UserId = testUserId },
            };

            // Setup: Repositório retorna as Entidades
            mockRepo.Setup(r => r.GetSummaryAsync(testUserId))
                     .Returns(System.Threading.Tasks.Task.FromResult(tasksFromRepo));


            var handler = new GetTaskSummaryQueryHandler(mockRepo.Object);
            var query = new GetTaskSummaryQuery { UserId = testUserId };

            // ACT
            // CORREÇÃO: Conversão para Lista para permitir a indexação
            var resultList = (await handler.Handle(query)).ToList();

            // ASSERT
            mockRepo.Verify(r => r.GetSummaryAsync(testUserId), Times.Once);
            Assert.Equal(2, resultList.Count);
            Assert.Equal("Daily Standup", resultList[0].Title);
        }
    }
}
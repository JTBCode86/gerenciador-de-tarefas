// Localização: Features/Tasks/Queries/GetTaskSummary/GetTaskSummaryQueryHandlerTests.cs

using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using TaskPilot.Domain.Interfaces;
using TaskPilot.Domain.Entities;
using TaskPilot.Application.Features.Tasks.Queries.GetTaskSummary;
using DomainTask = TaskPilot.Domain.Entities.Task;
using System.Threading; // Necessário para CancellationToken.None
using System;

namespace TaskPilot.Application.Tests.Features.Tasks.Queries.GetTaskSummary
{
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
                new DomainTask { Id = 1, Title = "Daily Standup", StatusId = 1, UserId = testUserId, DueDate = new DateTime(2025, 10, 20) },
                new DomainTask { Id = 2, Title = "Bug Fix", StatusId = 2, UserId = testUserId, DueDate = new DateTime(2025, 10, 25) },
            };

            // Setup: Repositório retorna as Entidades
            // Usa o método correto E o .AsEnumerable() para satisfazer o Task<IEnumerable<T>>
            mockRepo.Setup(r => r.GetSummaryTasksByUserIdAsync(testUserId))
                    .Returns(System.Threading.Tasks.Task.FromResult(tasksFromRepo.AsEnumerable()));

            var handler = new GetTaskSummaryQueryHandler(mockRepo.Object);
            var query = new GetTaskSummaryQuery { UserId = testUserId };

            // ACT
            // Nota: O handler MediatR Handle(query) deve ser chamado com o CancellationToken.
            // Assumindo que seu handler implementa IRequestHandler<GetTaskSummaryQuery, List<TaskSummaryDto>>
            var resultList = (await handler.Handle(query, CancellationToken.None)).ToList();

            // ASSERT

            // 1. Verifica se o método correto do repositório foi chamado (CORREÇÃO DA MENSAGEM DE ERRO)
            mockRepo.Verify(r => r.GetSummaryTasksByUserIdAsync(testUserId), Moq.Times.Once);

            // 2. Verifica a contagem de resultados
            Assert.Equal(2, resultList.Count);

            // 3. Verifica o primeiro item (Daily Standup, StatusId 1 => "Pendente")
            Assert.Equal(1, resultList[0].Id);
            Assert.Equal("Daily Standup", resultList[0].Title);
            Assert.Equal("Pendente", resultList[0].Status); // Assumindo lógica de mapeamento no Handler

            // 4. Verifica o segundo item (Bug Fix, StatusId 2 => "Concluído")
            Assert.Equal(2, resultList[1].Id);
            Assert.Equal("Bug Fix", resultList[1].Title);
            Assert.Equal("Concluído", resultList[1].Status); // Assumindo lógica de mapeamento no Handler
        }
    }
}
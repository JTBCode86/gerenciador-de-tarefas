// TaskPilot.Application/Features/Tasks/Queries/GetTaskSummary/GetTaskSummaryQueryHandler.cs
using TaskPilot.Domain.Interfaces;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TaskPilot.Application.Features.Tasks.Queries.GetTaskSummary
{
    public class GetTaskSummaryQueryHandler
    {
        private readonly ITaskRepository _taskRepository;

        public GetTaskSummaryQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<IEnumerable<TaskSummaryDto>> Handle(GetTaskSummaryQuery query)
        {
            // 1. Obter dados da Infraestrutura/BD (A ITaskRepository usará a View vw_UserTaskSummary aqui)
            var tasks = await _taskRepository.GetSummaryTasksByUserIdAsync(query.UserId);

            // 2. Mapeamento de Entidade para DTO
            var summaryList = tasks.Select(t => new TaskSummaryDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status?.Name ?? "Desconhecido", // Navegação de entidade
                Priority = t.PriorityLevel.ToString(),
                EstimatedDuration = t.EstimatedDuration,
                DueDate = t.DueDate,
                CompletedAt = t.CompletedAt,
                CreatedAt = t.CreatedAt,
                // Nota: O campo Categories precisaria de lógica de mapeamento aqui,
                // ou a ITaskRepository deve retornar um DTO de Infraestrutura que já inclua a string agrupada da View.
                // Assumiremos que a lógica de agregação de categorias vem da View do T-SQL.
                Categories = string.Join(", ", t.TaskCategoryMaps.Select(tcm => tcm.Category.Name)) // Mapeamento completo
            });

            return summaryList;
        }

        public async Task<List<TaskSummaryDto>> Handle(GetTaskSummaryQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _taskRepository.GetSummaryTasksByUserIdAsync(request.UserId);

            if (tasks == null)
            {
                return new List<TaskSummaryDto>(); // Evita NullReference, retorna 0
            }

            var summaryList = tasks.Select(t => new TaskSummaryDto
            {
                Id = t.Id,
                Title = t.Title,
                DueDate = t.DueDate,
                // Exemplo de Mapeamento de Status:
                Status = t.StatusId == 1 ? "Pendente" :
                         t.StatusId == 2 ? "Concluído" :
                         "Outro Status"

            }).ToList();

            return summaryList;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskPilot.Domain.Entities;

namespace TaskPilot.Domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<Entities.Task> GetByIdAsync(int id);
        Task<int> AddAsync(Entities.Task task);
        System.Threading.Tasks.Task UpdateAsync(Entities.Task task);
        System.Threading.Tasks.Task DeleteAsync(int id);

        // Consultas complexas usando Stored Procedures/Views
        Task<IEnumerable<Entities.Task>> GetOverdueTasksByUserIdAsync(int userId);

        Task<IEnumerable<Entities.Task>> GetSummaryTasksByUserIdAsync(int userId);

       // Task<List<System.Threading.Tasks.Task>> GetSummaryAsync(int userId);

        System.Threading.Tasks.Task<List<TaskPilot.Domain.Entities.Task>> GetSummaryAsync(int userId);
    }
}

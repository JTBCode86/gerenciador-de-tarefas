using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TaskPilot.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> AddAsync(T entity);
        System.Threading.Tasks.Task UpdateAsync(T entity);
        System.Threading.Tasks.Task DeleteAsync(int id);
    }
}

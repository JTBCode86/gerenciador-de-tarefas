// TaskPilot.Application/Interfaces/IExternalApiService.cs
using System;
using System.Threading.Tasks;

namespace TaskPilot.Application.Interfaces
{
    // Define a "porta" para a comunicação externa
    public interface IExternalApiService
    {
        // Método usado para notificar sobre tarefas de alta prioridade
        Task SendHighPriorityNotification(string title, DateTime? dueDate);
    }
}
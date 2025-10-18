using System;
using TaskPilot.Application.Interfaces; // Esta Interface é definida no projeto Application

namespace TaskPilot.Infrastructure.ExternalServices
{
    // A interface IExternalApiService deve ser definida no projeto TaskPilot.Application
    // public interface IExternalApiService { Task SendHighPriorityNotification(string title, DateTime? dueDate); }

    public class NotificationService : IExternalApiService
    {
        // Simulação de credenciais de serviço externo (ex: Twilio ou SendGrid)
        private readonly string _apiKey;

        public NotificationService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async System.Threading.Tasks.Task SendHighPriorityNotification(string title, DateTime? dueDate)
        {
            // Lógica real de integração com a API externa aqui

            // Exemplo de log para simular a chamada API
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"[EXTERNAL API CALL] Enviando notificação crítica (API Key: {_apiKey.Substring(0, 4)}...)");
            Console.WriteLine($"Título: '{title}' | Prazo: {dueDate}");
            Console.WriteLine("---------------------------------------------");

            // Simula uma chamada assíncrona
            await System.Threading.Tasks.Task.Delay(50);
        }
    }
}
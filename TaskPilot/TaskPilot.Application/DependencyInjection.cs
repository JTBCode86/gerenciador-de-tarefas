// TaskPilot.Application/DependencyInjection.cs
using Microsoft.Extensions.DependencyInjection;
using TaskPilot.Application.Features.Tasks.Commands.CreateTask;
using TaskPilot.Application.Features.Tasks.Commands.CompleteTask;
using TaskPilot.Application.Features.Tasks.Queries.GetTaskSummary;
using System.Reflection;

namespace TaskPilot.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Registra todos os Command Handlers e Query Handlers
            services.AddScoped<CreateTaskCommandHandler>();
            services.AddScoped<CompleteTaskCommandHandler>();
            services.AddScoped<GetTaskSummaryQueryHandler>();

            // Opcional: Adicionar AutoMapper se estiver usando mapeamento complexo
            // services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
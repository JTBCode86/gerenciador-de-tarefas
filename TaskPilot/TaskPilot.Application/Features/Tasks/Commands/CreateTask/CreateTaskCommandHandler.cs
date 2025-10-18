﻿// TaskPilot.Application/Features/Tasks/Commands/CreateTask/CreateTaskCommandHandler.cs
using TaskPilot.Application.Interfaces;
using TaskPilot.Domain.Entities;
using TaskPilot.Domain.Enums;
using TaskPilot.Domain.Interfaces;
using System.Threading.Tasks;
using System;

namespace TaskPilot.Application.Features.Tasks.Commands.CreateTask
{
    public class CreateTaskCommandHandler
    {
        // Dependências: Apenas interfaces do Domain (ITaskRepository) e Application (IExternalApiService)
        private readonly ITaskRepository _taskRepository;
        private readonly IExternalApiService _externalApiService;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, IExternalApiService externalApiService)
        {
            _taskRepository = taskRepository;
            _externalApiService = externalApiService;
        }

        public async Task<int> Handle(CreateTaskCommand command)
        {
            // 1. Lógica de Negócio (Mapeamento do Command para a Entidade do Domínio)
            var newTask = new Domain.Entities.Task
            {
                UserId = command.UserId,
                Title = command.Title,
                Description = command.Description,
                PriorityLevel = command.PriorityLevel,
                EstimatedDuration = command.EstimatedDuration,
                DueDate = command.DueDate,
                StatusId = 1, // 'Pendente' (Assumindo que 1 é o ID de Pendente no BD)
                CreatedAt = DateTime.Now
            };

            // 2. Persistência (Chama a interface, a Infrastructure fará o trabalho real)
            var newTaskId = await _taskRepository.AddAsync(newTask);

            // 3. Lógica de Notificação (Integração com o Adaptador Externo)
            if (newTask.PriorityLevel == PriorityLevel.High)
            {
                await _externalApiService.SendHighPriorityNotification(newTask.Title, newTask.DueDate);
            }

            return newTaskId;
        }
    }
}
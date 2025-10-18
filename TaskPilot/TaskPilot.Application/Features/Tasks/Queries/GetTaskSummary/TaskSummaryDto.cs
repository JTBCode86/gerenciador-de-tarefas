// TaskPilot.Application/Features/Tasks/Queries/GetTaskSummary/TaskSummaryDto.cs
using System;

namespace TaskPilot.Application.Features.Tasks.Queries.GetTaskSummary
{
    public class TaskSummaryDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // Nome do Status
        public string Priority { get; set; } // Nome da Prioridade
        public int? EstimatedDuration { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Categories { get; set; } // Lista de categorias (ex: "Trabalho, Estudo")
    }
}
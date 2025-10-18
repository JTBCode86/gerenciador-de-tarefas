using System;
using System.Collections.Generic;
using System.Text;
using TaskPilot.Domain.Enums;

namespace TaskPilot.Domain.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        // Usamos o Enum no domínio para tipagem forte
        public PriorityLevel PriorityLevel { get; set; }

        public int? EstimatedDuration { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        // Propriedades de Navegação
        public User User { get; set; }

        public TaskStatus Status { get; set; }
        public ICollection<TaskCategoryMap> TaskCategoryMaps { get; set; } = new List<TaskCategoryMap>();
    }
}

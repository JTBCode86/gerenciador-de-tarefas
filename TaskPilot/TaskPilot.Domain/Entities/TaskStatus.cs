using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPilot.Domain.Entities
{
    public class TaskStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Propriedade de Navegação: Um Status pode ter muitas Tarefas
        public ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}

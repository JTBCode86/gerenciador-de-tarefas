using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPilot.Domain.Entities
{
    public class TaskCategoryMap
    {
        // Chaves que formam a Chave Composta no T-SQL
        public int TaskId { get; set; }
        public int CategoryId { get; set; }

        // Propriedades de Navegação
        public Task Task { get; set; }
        public TaskCategory Category { get; set; }
    }
}

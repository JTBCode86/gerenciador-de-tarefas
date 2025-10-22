using System;
using System.Collections.Generic;
using System.Text;

namespace TaskPilot.Domain.Entities
{
    public class TaskCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Propriedade de Navegação: Relacionamento M:N via TaskCategoryMap
        public ICollection<TaskCategoryMap> TaskCategoryMaps { get; set; } = new List<TaskCategoryMap>();
    }
}

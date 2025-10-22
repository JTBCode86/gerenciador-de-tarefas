using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskPilot.Domain.Entities;

namespace TaskPilot.Infrastructure.Data.Configurations
{
    public class TaskCategoryMapConfiguration : IEntityTypeConfiguration<TaskCategoryMap>
    {
        public void Configure(EntityTypeBuilder<TaskCategoryMap> builder)
        {
            builder.ToTable("TaskCategoryMap"); // Nome da tabela de junção

            // Define a Chave Primária Composta
            builder.HasKey(tcm => new { tcm.TaskId, tcm.CategoryId });

            // Configura os relacionamentos
            builder.HasOne(tcm => tcm.Task)
                   .WithMany(t => t.TaskCategoryMaps)
                   .HasForeignKey(tcm => tcm.TaskId);

            builder.HasOne(tcm => tcm.Category)
                   .WithMany(tc => tc.TaskCategoryMaps)
                   .HasForeignKey(tcm => tcm.CategoryId);
        }
    }
}
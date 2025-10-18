using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskPilot.Domain.Enums;

namespace TaskPilot.Infrastructure.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Domain.Entities.Task>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Task> builder)
        {
            builder.ToTable("Tasks"); // Garante o nome da tabela no T-SQL

            // Mapeia o Enum 'PriorityLevel' para um INT no banco
            builder.Property(t => t.PriorityLevel)
                   .HasConversion<int>()
                   .IsRequired();

            // Configuração da Chave Estrangeira
            builder.HasOne(t => t.User)
                   .WithMany(u => u.Tasks)
                   .HasForeignKey(t => t.UserId)
                   .IsRequired();

            // Propriedades de Data
            builder.Property(t => t.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(t => t.CompletedAt).IsRequired(false);
        }
    }
}

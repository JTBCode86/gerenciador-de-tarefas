// TaskPilot.Application/Features/Tasks/Commands/CompleteTask/CompleteTaskCommand.cs
namespace TaskPilot.Application.Features.Tasks.Commands.CompleteTask
{
    public class CompleteTaskCommand
    {
        public int TaskId { get; set; }
        public int UserId { get; set; } // Para verificação de segurança (o usuário deve ser o dono)
    }
}
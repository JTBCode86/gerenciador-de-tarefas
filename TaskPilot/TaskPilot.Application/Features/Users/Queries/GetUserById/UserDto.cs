// Localização: TaskPilot.Application/Features/Users/Queries/GetUserById/UserDto.cs

namespace TaskPilot.Application.Features.Users.Queries.GetUserById
{
    public class UserDto
    {
        // Identificador do Usuário
        public int Id { get; set; }

        // Nome de Usuário (Username)
        public string FirstName { get; set; }

        // Nome Completo (Opcional, se você tiver no Domain)
        public string Email { get; set; }

    }
}
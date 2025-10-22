using System.Threading.Tasks;
using TaskPilot.Application.Features.Users.Queries.GetUserById;

namespace TaskPilot.Application.Interfaces
{
    // O DTO (UserDto) e a Query (GetUserByIdQuery) continuam existindo
    // Mas agora são chamados por um Service.
    public interface IUserService
    {
        // O método recebe a Query e retorna o DTO
        Task<UserDto> GetUserByIdAsync(GetUserByIdQuery query);
        // Task<int> CreateUserAsync(CreateUserCommand command);
    }
}
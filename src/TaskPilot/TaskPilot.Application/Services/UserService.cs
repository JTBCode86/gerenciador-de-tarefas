// Este seria o substituto dos Handlers do MediatR
using System.Threading.Tasks;
using TaskPilot.Application.Features.Users.Queries.GetUserById;
using TaskPilot.Application.Interfaces;
using TaskPilot.Domain.Interfaces;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> GetUserByIdAsync(GetUserByIdQuery query)
    {
        // 1. Lógica de validação...

        // 2. Busca do repositório (o que o Handler faria)
        var user = await _userRepository.GetByIdAsync(query.UserId);

        if (user == null) return null;

        // 3. Mapeamento para DTO
        return new UserDto { Id = user.Id, FirstName = user.FirstName };
    }
}
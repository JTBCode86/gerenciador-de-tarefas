using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TaskPilot.Domain.Interfaces;

namespace TaskPilot.Application.Features.Users.Queries.GetUserById
{
    // O Handler recebe a Query e retorna o DTO.
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        // Adicione o IRepository correto aqui. (Ex: ITaskRepository para Tasks)

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Método principal do MediatR
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            // 1. Busca o objeto de domínio/entidade no banco de dados.
            var user = await _userRepository.GetByIdAsync(request.UserId);

            // 2. Se o usuário não existir, retorna null.
            if (user == null)
            {
                // Retornar null aqui faz com que o UsersController retorne 404 Not Found.
                return null;
            }

            // 3. Mapeia a entidade (Domain) para o DTO (Application).
            // Você pode usar AutoMapper aqui, mas faremos o mapeamento manual para simplicidade.
            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName
                // Adicione outras propriedades do DTO aqui...
            };

            return userDto;
        }
    }
}
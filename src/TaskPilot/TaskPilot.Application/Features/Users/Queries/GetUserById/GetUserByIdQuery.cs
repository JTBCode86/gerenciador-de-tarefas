using MediatR;
// Garanta que você tenha a referência ao DTO (a resposta esperada)
using TaskPilot.Application.Features.Users.Queries.GetUserById;

namespace TaskPilot.Application.Features.Users.Queries.GetUserById
{
    // IRequest<UserDto> indica que esta query espera um UserDto como resultado.
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        // O ID que será usado pelo Handler para buscar o usuário no banco de dados.
        public int UserId { get; set; }
    }
}
// TaskPilot.API/Controllers/UsersController.cs (Sem MediatR)
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskPilot.Application.Features.Users.Queries.GetUserById;
using TaskPilot.Application.Interfaces; // Novo using

public class UsersController : ControllerBase
{
    // ✅ Injeta o Serviço de Aplicação
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var query = new GetUserByIdQuery { UserId = id };

        var result = await _userService.GetUserByIdAsync(query);

        if (result == null) return NotFound();
        return Ok(result);
    }
}
using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.Users.Commands.CreateUser;
using Notes.Data.Features.Users.Commands.GetUserToken;

namespace Notes.Web.Controllers;

[Route("api/users")]
public sealed class UsersController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetUserTokenAsync(
        [FromBody] GetUserTokenDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetUserTokenCommand(dto), cancellationToken));
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(
        [FromBody] CreateUserDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CreateUserCommand(dto), cancellationToken));
    }
}
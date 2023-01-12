using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.Users.Commands.CreateUser;
using Notes.Data.Features.Users.Commands.GetUserToken;

namespace Notes.Web.Controllers;

/// <summary>
///     Контроллер пользователей
/// </summary>
[Route("api/users")]
public sealed class UsersController : ApiControllerBase
{
    /// <summary>
    ///     Запрос аунтентификации пользователя
    /// </summary>
    /// <param name="dto">Данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Jwt токен</returns>
    [HttpGet]
    public async Task<IActionResult> GetUserTokenAsync(
        [FromQuery] GetUserTokenDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetUserTokenCommand(dto), cancellationToken));
    }

    /// <summary>
    ///     Запрос на создание нового пользователя
    /// </summary>
    /// <param name="dto">Данные пользователя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Jwt токен</returns>
    [HttpPost]
    public async Task<IActionResult> RegisterUserAsync(
        [FromBody] CreateUserDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new CreateUserCommand(dto), cancellationToken));
    }
}
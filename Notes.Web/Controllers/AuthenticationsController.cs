using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.Authentications.Commands.Authenticate;

namespace Notes.Web.Controllers;

[Route("api/authentication")]
public sealed class AuthenticationsController : Controller
{
    [HttpPost]
    public async Task<IActionResult> AuthenticateAsync(
        [FromBody] AuthenticateDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        var any = User.Claims.Any(c => c.Type == "Email");

        return Ok(await Mediator.Send(new AuthenticateCommand(dto), cancellationToken));
    }
}
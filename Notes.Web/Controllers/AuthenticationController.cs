using Microsoft.AspNetCore.Mvc;
using Notes.Data.Features.Authentications.Commands.Authenticate;

namespace Notes.Web.Controllers;

[Route("api/authentication")]
public sealed class AuthenticationController : Controller
{
    [HttpPost]
    public async Task<IActionResult> AuthenticateAsync(
        [FromBody] AuthenticateDto dto,
        [FromQuery] CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new AuthenticateCommand(dto), cancellationToken));
    }
}
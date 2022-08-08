using Microsoft.AspNetCore.Mvc;
using Notes.Data.Services;

namespace Notes.Web.Controllers;

[Route("api/emails")]
public class EmailsController : Controller
{
    private readonly EmailService _emailService;

    public EmailsController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmailAsync(
        [FromBody] MessageDto message,
        [FromQuery] CancellationToken cancellationToken)
    {
        await _emailService.Send(
            message.To,
            message.Subject!,
            message.Html!,
            cancellationToken);

        return NoContent();
    }
}
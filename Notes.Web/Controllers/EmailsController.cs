using Microsoft.AspNetCore.Mvc;
using Notes.Data.Services.Emails;

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
        [FromBody] EmailDto email,
        [FromQuery] CancellationToken cancellationToken)
    {
        await _emailService.SendAsync(
            email.To,
            email.Subject!,
            email.Html!,
            cancellationToken);

        return NoContent();
    }
}
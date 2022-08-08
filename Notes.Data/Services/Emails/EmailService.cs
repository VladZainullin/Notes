using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Notes.Data.Services.Emails;

namespace Notes.Data.Services;

public sealed class EmailService
{
    private readonly IOptions<EmailOptions> _options;

    public EmailService(IOptions<EmailOptions> options)
    {
        _options = options;
    }

    public async Task Send(
        string to,
        string subject,
        string html,
        CancellationToken cancellationToken)
    {
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_options.Value.Name));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = html
        };

        // send email
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _options.Value.Host,
            _options.Value.Port,
            SecureSocketOptions.StartTls,
            cancellationToken);
        await smtp.AuthenticateAsync(
            _options.Value.Name,
            _options.Value.Password,
            cancellationToken);
        await smtp.SendAsync(email, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}
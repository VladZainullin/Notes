using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Notes.Data.Services.Emails;

/// <summary>
/// Сервис отправки писем на электронную почту
/// </summary>
public sealed class EmailService
{
    /// <summary>
    /// Опции отправки сообщения
    /// </summary>
    private readonly IOptions<EmailOptions> _options;

    /// <summary>
    /// конструктор сервиса отправки писем на электронную почту
    /// </summary>
    /// <param name="options"></param>
    public EmailService(IOptions<EmailOptions> options)
    {
        _options = options;
    }

    /// <summary>
    /// Метод отправки сообщения на электронную почту
    /// по указанным параметрам
    /// </summary>
    /// <param name="to">Адрес электронной почты получателя</param>
    /// <param name="subject">Тема письма</param>
    /// <param name="html">Содержимое письма в формате HTML</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>>
    public async Task SendAsync(
        string to,
        string subject,
        string html,
        CancellationToken cancellationToken)
    {
        // create message
        var email = CreateMessage(to, subject, html);

        // send email
        await SendEmailAsync(email, cancellationToken);
    }

    /// <summary>
    /// Метод создания письма
    /// </summary>
    /// <param name="to">Адрес электронной почты получатетеля</param>
    /// <param name="subject">Тема письма</param>
    /// <param name="html">Содержимое письма в формате HTML</param>
    /// <returns>Сформированное письмо</returns>
    private MimeMessage CreateMessage(
        in string to, 
        in string subject,
        in string html)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_options.Value.From));
        email.To.Add(MailboxAddress.Parse(to));
        email.Date = DateTimeOffset.Now.LocalDateTime;
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html)
        {
            Text = html
        };
        return email;
    }

    /// <summary>
    /// Метод отправки сообщения на электронную почту
    /// </summary>
    /// <param name="email">Письмо</param>
    /// <param name="cancellationToken">Токен отмены</param>
    private async Task SendEmailAsync(
        MimeMessage email,
        CancellationToken cancellationToken)
    {
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _options.Value.Host,
            _options.Value.Port,
            SecureSocketOptions.StartTls,
            cancellationToken);
        
        await smtp.AuthenticateAsync(
            _options.Value.From,
            _options.Value.Password,
            cancellationToken);
        
        await smtp.SendAsync(email, cancellationToken);
        await smtp.DisconnectAsync(true, cancellationToken);
    }
}
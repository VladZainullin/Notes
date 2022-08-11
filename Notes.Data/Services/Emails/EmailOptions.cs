namespace Notes.Data.Services.Emails;

/// <summary>
///     Опции сервиса отправки писем на электронную почту
/// </summary>
public sealed class EmailOptions
{
    /// <summary>
    ///     Адрес электронной почты отправителя
    /// </summary>
    public string? From { get; set; }

    /// <summary>
    ///     Пароль электронной почты отправителя
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    ///     Хост сервера
    /// </summary>
    public string? Host { get; set; }

    /// <summary>
    ///     Порт на сервере
    /// </summary>
    public int Port { get; set; }
}
namespace Notes.Data.Services.Emails;

/// <summary>
///     ДТО электронного письма
/// </summary>
public sealed class EmailDto
{
    /// <summary>
    ///     Адрес электронной почты получателя
    /// </summary>
    public string To { get; set; } = null!;

    /// <summary>
    ///     Тема письма
    /// </summary>
    public string Subject { get; set; } = null!;

    /// <summary>
    ///     Содержимое письма в формате HTML
    /// </summary>
    public string Html { get; set; } = null!;
}
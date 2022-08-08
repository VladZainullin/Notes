namespace Notes.Data.Services;

public sealed class MessageDto
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Html { get; set; } = null!;
}
namespace Notes.Data.Services.Emails;

public sealed class EmailOptions
{
    public string? Name { get; set; }

    public string? Password { get; set; }

    public string? Host { get; set; }

    public int Port { get; set; }
}
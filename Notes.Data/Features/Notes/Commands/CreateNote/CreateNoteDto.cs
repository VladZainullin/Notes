namespace Notes.Data.Features.Notes.Commands.CreateNote;

public sealed record CreateNoteDto
{
    public string? Header { get; init; }

    public string? Body { get; init; }
}
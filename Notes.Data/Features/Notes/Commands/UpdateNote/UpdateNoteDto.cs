namespace Notes.Data.Features.Notes.Commands.UpdateNote;

public sealed record UpdateNoteDto
{
    public string? Header { get; init; }

    public string? Body { get; init; }
}
namespace Notes.Data.Features.Notes.Queries.GetNotes;

internal sealed record GetNotesDto
{
    public int Id { get; init; }

    public string? Header { get; init; }

    public string? Body { get; init; }

    public bool IsPinned { get; set; }
}
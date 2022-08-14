namespace Notes.Data.Features.Notes.Queries.GetNote;

internal sealed record GetNoteDto
{
    public int Id { get; init; }

    public string? Header { get; init; }

    public string? Body { get; init; }

    public bool IsPinned { get; set; }
}
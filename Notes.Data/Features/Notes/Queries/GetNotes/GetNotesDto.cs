namespace Notes.Data.Features.Notes.Queries.GetNotes;

internal sealed class GetNotesDto
{
    public int Id { get; set; }

    public string? Header { get; set; }

    public string? Body { get; set; }
}
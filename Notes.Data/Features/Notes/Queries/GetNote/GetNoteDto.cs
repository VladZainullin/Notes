namespace Notes.Data.Features.Notes.Queries.GetNote;

internal sealed class GetNoteDto
{
    public int Id { get; set; }

    public string? Header { get; set; }

    public string? Body { get; set; }
}
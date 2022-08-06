namespace Notes.Data.Features.NoteLabels.Queries.GetNoteLabels;

internal sealed record GetNoteLabelsDto
{
    public int NoteLabelId { get; set; }

    public string? LabelTitle { get; set; }
}
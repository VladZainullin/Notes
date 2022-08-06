namespace Notes.Data.Features.Labels.Queries.GetLabel;

internal sealed record GetLabelDto
{
    public int Id { get; set; }
    
    public string? Title { get; init; }
}
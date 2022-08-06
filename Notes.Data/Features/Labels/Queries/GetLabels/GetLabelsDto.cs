namespace Notes.Data.Features.Labels.Queries.GetLabels;

internal sealed record GetLabelsDto
{
    public int Id { get; set; }
    
    public string? Title { get; init; }
}
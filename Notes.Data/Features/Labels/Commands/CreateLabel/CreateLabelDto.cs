namespace Notes.Data.Features.Labels.Commands.CreateLabel;

public sealed record CreateLabelDto
{
    public string? Title { get; init; }
}
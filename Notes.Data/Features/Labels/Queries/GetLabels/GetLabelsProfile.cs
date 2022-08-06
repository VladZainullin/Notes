using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Labels.Queries.GetLabels;

internal sealed class GetLabelsProfile : Profile
{
    public GetLabelsProfile()
    {
        CreateMap<Label, GetLabelsDto>();
    }
}
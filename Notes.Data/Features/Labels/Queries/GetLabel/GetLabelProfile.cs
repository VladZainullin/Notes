using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Labels.Queries.GetLabel;

internal sealed class GetLabelProfile : Profile
{
    public GetLabelProfile()
    {
        CreateMap<Label, GetLabelDto>();
    }
}
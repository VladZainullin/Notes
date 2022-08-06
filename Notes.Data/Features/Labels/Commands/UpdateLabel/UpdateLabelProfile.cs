using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Labels.Commands.UpdateLabel;

internal sealed class UpdateLabelProfile : Profile
{
    public UpdateLabelProfile()
    {
        CreateMap<UpdateLabelDto, Label>();
    }
}
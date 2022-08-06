using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Labels.Commands.CreateLabel;

internal sealed class CreateLabelProfile : Profile
{
    public CreateLabelProfile()
    {
        CreateMap<CreateLabelDto, Label>();
    }
}
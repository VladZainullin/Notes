using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Notes.Commands.UpdateNote;

internal sealed class UpdateNoteProfile : Profile
{
    public UpdateNoteProfile()
    {
        CreateMap<UpdateNoteDto, Note>();
    }
}
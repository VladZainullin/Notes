using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Notes.Commands.UpdateNote;

internal class UpdateNoteProfile : Profile
{
    public UpdateNoteProfile()
    {
        CreateMap<UpdateNoteDto, Note>();
    }
}
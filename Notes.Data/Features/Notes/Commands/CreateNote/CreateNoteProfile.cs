using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Notes.Commands.CreateNote;

internal sealed class CreateNoteProfile : Profile
{
    public CreateNoteProfile()
    {
        CreateMap<CreateNoteDto, Note>();
    }
}
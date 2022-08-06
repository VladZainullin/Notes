using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.NoteLabels.Commands.CreateNoteLabel;

internal sealed class CreateNoteLabelProfile : Profile
{
    public CreateNoteLabelProfile()
    {
        CreateMap<CreateNoteLabelCommand, NoteLabel>();
    }
}
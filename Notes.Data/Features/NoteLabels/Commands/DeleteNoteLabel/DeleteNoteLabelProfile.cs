using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.NoteLabels.Commands.DeleteNoteLabel;

internal sealed class DeleteNoteLabelProfile : Profile
{
    public DeleteNoteLabelProfile()
    {
        CreateMap<DeleteNoteLabelCommand, NoteLabel>();
    }
}
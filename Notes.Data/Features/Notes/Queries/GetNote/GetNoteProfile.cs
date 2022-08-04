using AutoMapper;
using Notes.Core.Entities;
using Notes.Data.Features.Notes.Queries.GetNotes;

namespace Notes.Data.Features.Notes.Queries.GetNote;

internal class GetNoteProfile : Profile
{
    public GetNoteProfile()
    {
        CreateMap<Note, GetNotesDto>();
    }
}
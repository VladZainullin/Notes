using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Notes.Queries.GetNotes;

internal class GetNotesProfile : Profile
{
    public GetNotesProfile()
    {
        CreateMap<Note, GetNotesDto>();
    }
}
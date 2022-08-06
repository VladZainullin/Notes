using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Notes.Queries.GetNotes;

internal sealed class GetNotesProfile : Profile
{
    public GetNotesProfile()
    {
        CreateMap<Note, GetNotesDto>();
    }
}
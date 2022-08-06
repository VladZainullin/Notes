using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Notes.Queries.GetNote;

internal sealed class GetNoteProfile : Profile
{
    public GetNoteProfile()
    {
        CreateMap<Note, GetNoteDto>();
    }
}
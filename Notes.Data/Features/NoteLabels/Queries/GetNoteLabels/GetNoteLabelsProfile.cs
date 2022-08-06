using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.NoteLabels.Queries.GetNoteLabels;

internal sealed class GetNoteLabelsProfile : Profile
{
    public GetNoteLabelsProfile()
    {
        CreateMap<NoteLabel, GetNoteLabelsDto>()
            .ForMember(dto => dto.LabelTitle, configuration =>
                configuration.MapFrom(nl => nl.Label!.Title))
            .ForMember(dto => dto.NoteLabelId, configuration =>
                configuration.MapFrom(nl => nl.LabelId));
    }
}
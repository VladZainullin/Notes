using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Users.Commands.GetUserToken;

internal sealed class GetUserTokenProfile : Profile
{
    public GetUserTokenProfile()
    {
        CreateMap<GetUserTokenDto, User>();
    }
}
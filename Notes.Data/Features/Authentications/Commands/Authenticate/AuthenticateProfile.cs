using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Authentications.Commands.Authenticate;

internal sealed class AuthenticateProfile : Profile
{
    public AuthenticateProfile()
    {
        CreateMap<AuthenticateDto, User>();
    }
}
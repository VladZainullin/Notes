using AutoMapper;
using Notes.Core.Entities;

namespace Notes.Data.Features.Users.Commands.CreateUser;

public sealed class CreateUserProfile : Profile
{
    public CreateUserProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}
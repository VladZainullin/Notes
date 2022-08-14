namespace Notes.Data.Features.Users.Commands.CreateUser;

public sealed record CreateUserDto(
    string Name,
    string Surname,
    string Patronymic,
    DateTime DateOfBirth,
    string Login,
    string Password,
    string RepeatPassword);
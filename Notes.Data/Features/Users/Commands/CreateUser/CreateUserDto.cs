namespace Notes.Data.Features.Users.Commands.CreateUser;

public sealed record CreateUserDto(
    string Name,
    string Surname,
    string Patronymic,
    DateTime DateOfBirth,
    string Email,
    string Password,
    string RepeatPassword);
namespace Notes.Data.Features.Users.Commands.GetUserToken;

public sealed record GetUserTokenDto(
    string Email,
    string Password);
namespace Notes.Data.Features.Authentications.Commands.Authenticate;

public sealed record AuthenticateDto(
    string Login,
    string Password);
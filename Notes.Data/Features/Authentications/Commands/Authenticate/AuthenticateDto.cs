namespace Notes.Data.Features.Authentications.Commands.Authenticate;

public sealed record AuthenticateDto(
    string Email,
    string Password);
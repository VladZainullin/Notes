using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Contexts;
using Notes.Data.Exceptions;
using Notes.Data.Services.JwtTokenServices;

namespace Notes.Data.Features.Users.Commands.GetUserToken;

public sealed record GetUserTokenCommand(GetUserTokenDto Dto) : IRequest<string>;

internal sealed class GetUserTokenHandler : IRequestHandler<GetUserTokenCommand, string>
{
    private readonly AppDbContext _context;
    private readonly JwtSecurityTokenService _jwtSecurityTokenService;

    public GetUserTokenHandler(
        AppDbContext context,
        JwtSecurityTokenService jwtSecurityTokenService)
    {
        _context = context;
        _jwtSecurityTokenService = jwtSecurityTokenService;
    }

    public async Task<string> Handle(
        GetUserTokenCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await IsExistsUserAsync(
            request.Dto.Email,
            cancellationToken);
        if (!exists)
            throw new BadRequestException("Аккаунт не зарегистрирован");

        var user = await GetUserAsync(request.Dto.Email, cancellationToken);

        var access = user.Password == request.Dto.Password;
        if (!access)
            throw new BadRequestException("Неправильный пароль");

        var token = _jwtSecurityTokenService.Create(user);
        return token;
    }

    private async Task<bool> IsExistsUserAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    private async Task<User> GetUserAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context.Users
            .AsNoTracking()
            .SingleAsync(u => u.Email == email, cancellationToken);
    }
}
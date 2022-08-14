using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;
using Notes.Data.Services.JwtTokenServices;
using Notes.Data.Services.Users;

namespace Notes.Data.Features.Users.Commands.GetUserToken;

public sealed record GetUserTokenCommand(GetUserTokenDto Dto) : IRequest<string>;

internal sealed class GetUserTokenHandler : IRequestHandler<GetUserTokenCommand, string>
{
    private readonly DbContext _context;
    private readonly CurrentUserService _currentUserService;
    private readonly JwtSecurityTokenService _jwtSecurityTokenService;
    private readonly IMapper _mapper;

    public GetUserTokenHandler(
        DbContext context,
        CurrentUserService currentUserService,
        JwtSecurityTokenService jwtSecurityTokenService,
        IMapper mapper)
    {
        _context = context;
        _currentUserService = currentUserService;
        _jwtSecurityTokenService = jwtSecurityTokenService;
        _mapper = mapper;
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
        return await _context
            .Set<User>()
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);
    }

    private async Task<User> GetUserAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context
            .Set<User>()
            .AsNoTracking()
            .SingleAsync(u => u.Email == email, cancellationToken);
    }
}
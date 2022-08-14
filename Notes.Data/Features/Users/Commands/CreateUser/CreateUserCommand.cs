using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Core.Entities;
using Notes.Data.Exceptions;
using Notes.Data.Services.JwtTokenServices;

namespace Notes.Data.Features.Users.Commands.CreateUser;

public sealed record CreateUserCommand(CreateUserDto Dto) : IRequest<string>;

internal sealed class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
{
    private readonly DbContext _context;
    private readonly JwtSecurityTokenService _jwtSecurityTokenService;
    private readonly IMapper _mapper;

    public CreateUserHandler(
        DbContext context,
        JwtSecurityTokenService jwtSecurityTokenService,
        IMapper mapper)
    {
        _context = context;
        _jwtSecurityTokenService = jwtSecurityTokenService;
        _mapper = mapper;
    }

    public async Task<string> Handle(
        CreateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.Dto);

        var exists = await IsExistsUserEmailAsync(
            user.Email!,
            cancellationToken);
        if (exists)
            throw new BadRequestException("По данному почтовому ящику уже зарегистрирован аккаунт");

        await _context.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtSecurityTokenService.Create(user);

        return token;
    }

    private async Task<bool> IsExistsUserEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        var exists = await _context
            .Set<User>()
            .AsNoTracking()
            .AnyAsync(u => u.Email == email, cancellationToken);

        return exists;
    }
}
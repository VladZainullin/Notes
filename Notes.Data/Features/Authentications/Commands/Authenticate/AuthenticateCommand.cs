using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notes.Core.Entities;
using Notes.Data.Exceptions;

namespace Notes.Data.Features.Authentications.Commands.Authenticate;

public sealed record AuthenticateCommand(AuthenticateDto Dto) : IRequest<string>;

internal sealed class AuthenticateHandler : IRequestHandler<AuthenticateCommand, string>
{
    private readonly DbContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public AuthenticateHandler(
        DbContext context, 
        IMapper mapper,
        IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }
    
    public async Task<string> Handle(
        AuthenticateCommand request,
        CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request.Dto);

        var exists = await _context
            .Set<User>()
            .AnyAsync(u => u.Email == user.Email, cancellationToken);
        if (exists)
            throw new BadRequestException("По данному почтовому ящику уже зарегистрирован аккаунт");

        await _context.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        var security = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_configuration["Authentications:Security"]));

        var signingCredentials = new SigningCredentials(
            security,
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new("Id", user.Id.ToString()),
            new("Email", user.Email!),
            new("Password", user.Password!)
        };

        var token = new JwtSecurityToken(
            _configuration["Authentications:Issuer"],
            _configuration["Authentications:Audience"],
            claims,
            DateTime.Now,
            DateTime.Now.AddHours(1),
            signingCredentials);

        var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenToReturn;
    }
}
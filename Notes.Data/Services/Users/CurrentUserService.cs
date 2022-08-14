using Microsoft.AspNetCore.Http;

namespace Notes.Data.Services.Users;

public class CurrentUserService
{
    private readonly HttpContext _context;

    public CurrentUserService(HttpContext context)
    {
        _context = context;
    }

    public int Id => int.Parse(_context.User.Claims.FirstOrDefault(c => c.Type == "Id")!.Value);
}
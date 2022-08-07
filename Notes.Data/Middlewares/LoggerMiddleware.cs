using Microsoft.AspNetCore.Http;
using Serilog;

namespace Notes.Data.Middlewares;

public class LoggerMiddleware : IMiddleware
{
    private readonly ILogger _logger;

    public LoggerMiddleware(ILogger logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(
        HttpContext context,
        RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.Error(e.ToString());
            throw;
        }
    }
}
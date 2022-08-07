using System.Net;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Notes.Data.Exceptions;

namespace Notes.Data.Middlewares;

public sealed class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BadRequestException e)
        {
            await GetExceptionResult(
                context, 
                HttpStatusCode.BadRequest,
                e.Message);
        }
        catch (NotFoundException e)
        {
            await GetExceptionResult(
                context, 
                HttpStatusCode.NotFound,
                e.Message);
        }
        catch (Exception)
        {
            await GetExceptionResult(
                context, 
                HttpStatusCode.InternalServerError,
                "Ошибка сервера");
        }
    }

    private static async Task GetExceptionResult(
        HttpContext context,
        HttpStatusCode statusCode,
        string message)
    {
        context.Response.StatusCode = Convert.ToInt32(statusCode);
        context.Response.ContentType = "application/json";
        var result = JsonConvert.SerializeObject(new
        {
            context.Response.StatusCode,
            ErrorMessage = message
        });
        await context.Response.WriteAsync(result);
    }
}
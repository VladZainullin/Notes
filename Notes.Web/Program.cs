using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Data.Contexts;
using Notes.Data.Middlewares;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddScoped<LoggerMiddleware>();
builder.Services.AddScoped<ExceptionMiddleware>();

#endregion

#region MediatR

builder.Services.AddMediatR(Assembly.Load("Notes.Data"));

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(Assembly.Load("Notes.Data"));

#endregion

#region Serilog

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddSingleton(Log.Logger);

#endregion

#region Connections

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddScoped<DbContext, AppDbContext>();

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen();

#endregion

var app = builder.Build();

#region Serilog

app.UseSerilogRequestLogging();

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

#region Middlewares

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggerMiddleware>();

#endregion

app.MapControllers();

app.Run();
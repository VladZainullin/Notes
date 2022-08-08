using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Data.Contexts;
using Notes.Data.Middlewares;
using Notes.Data.Services;
using Serilog;
using Serilog.Events;
using StackExchange.Profiling;
using StackExchange.Profiling.Storage;

var builder = WebApplication.CreateBuilder(args);

#region Options

builder.Services.AddOptions();
builder.Services.Configure<EmailOptions>(builder.Configuration.GetSection("Email"));

#endregion

#region Mini profiler

builder.Services
    .AddMiniProfiler(options =>
    {
        options.ColorScheme = ColorScheme.Dark;
        options.RouteBasePath = "/mini-profiler"; //https://localhost:7199/mini-profiler/results-index
        (options.Storage as MemoryCacheStorage)!.CacheDuration = TimeSpan.FromMinutes(15);
    })
    .AddEntityFramework();

#endregion

#region Services

builder.Services.AddScoped<LoggerMiddleware>();
builder.Services.AddScoped<ExceptionMiddleware>();
builder.Services.AddScoped<EmailService>();

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

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
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
    app.UseMiniProfiler();
}

app.UseHttpsRedirection();

app.UseAuthorization();

#region Middlewares

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggerMiddleware>();

#endregion

app.MapControllers();

app.Run();
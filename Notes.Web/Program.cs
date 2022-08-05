using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Data.Contexts;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

#region MediatR

builder.Services.AddMediatR(Assembly.Load("Notes.Data"));

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
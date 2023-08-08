using System.Reflection;
using System.Text;
using Hangfire;
using Hangfire.MemoryStorage;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Notes.Data.Contexts;
using Notes.Data.Middlewares;
using Notes.Data.Services.Emails;
using Notes.Data.Services.JwtTokenServices;
using Notes.Data.Services.Users;
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
builder.Services.AddScoped<TransactionMiddleware>();

builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<JwtSecurityTokenService>();
builder.Services.AddScoped<CurrentUserService>();

builder.Services.AddScoped<HttpContextAccessor>();

builder.Services.AddHttpContextAccessor();

#endregion

#region MediatR

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region AutoMapper

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
builder.Services.AddScoped<AppDbContext>();

#endregion

#region Hangfire

builder.Services.AddHangfire(x => { x.UseMemoryStorage(); });
builder.Services.AddHangfireServer();

#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Swagger

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

#endregion

#region Jwt bearer

builder.Services
    .AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentications:Issuer"],
            ValidAudience = builder.Configuration["Authentications:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(builder.Configuration["Authentications:Security"]))
        };
    });

#endregion

#region Authentication policy

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("register", policy => { policy.RequireAuthenticatedUser(); });
});

#endregion

var app = builder.Build();

app.UseCors(b => b
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

#region Serilog

app.UseSerilogRequestLogging();

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    #region Swagger

    app.UseSwagger();
    app.UseSwaggerUI();

    #endregion

    #region Mini profiler

    app.UseMiniProfiler();

    #endregion

    app.UseHsts();
}

app.UseHttpsRedirection();

#region Authorization and Authentication

app.UseAuthentication();
app.UseAuthorization();

#endregion

#region Middlewares

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<LoggerMiddleware>();
app.UseMiddleware<TransactionMiddleware>();

#endregion

app.MapControllers();

#region Hangfire

app.UseHangfireDashboard();

#endregion

app.Run();
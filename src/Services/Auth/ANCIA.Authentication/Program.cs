using ANCIA.Authentication.Configuration;
using ANCIA.Authentication.Infra.IoC.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.AddApiConfiguration();
builder.Services.AddDependencyInjection(builder.Configuration);
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddAuthenticationConfiguration(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration();

app.Run();

using ANCIA.Authentication.Domain.Token;
using ANCIA.Authentication.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();

builder.Services.Configure<TokenRules>(builder.Configuration.GetSection("TokenRules"));

// Add services to the container.
builder.Services.ConfigureAppDependencies(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHsts();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

using QingFa.EShop.Api;
using QingFa.EShop.Application;
using QingFa.EShop.Infrastructure;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Verbose()
          .Enrich.FromLogContext()
          .WriteTo.Console(outputTemplate:
              "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
              theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Literate)
          .CreateLogger();


builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication()
                .AddInfrastructure()
                .AddPresentation();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();

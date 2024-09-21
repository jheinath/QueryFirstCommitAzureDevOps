using Adapters.AzureDevOps;
using Application;
using QueryFirstCommitAzureDevOps;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("Configuration/appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddOptions<Configuration>()
    .Bind(builder.Configuration.GetSection("configuration"))
    .ValidateDataAnnotations();
builder.Services.AddHostedService<Startup>();
builder.Services.AddApplicationServices();
builder.Services.AddAzureDevopsAdapter();

var app = builder.Build();
app.Run();

Console.WriteLine();
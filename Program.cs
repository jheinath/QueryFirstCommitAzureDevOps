using QueryFirstCommitAzureDevOps;
using QueryFirstCommitAzureDevOps.Configuration;
using QueryFirstCommitAzureDevOps.Queries;
using QueryFirstCommitAzureDevOps.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("Configuration/appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddOptions<Configuration>()
    .Bind(builder.Configuration.GetSection("configuration"))
    .ValidateDataAnnotations();
builder.Services.Configure<Configuration>(builder.Configuration.GetSection("configuration"));
builder.Services.AddHostedService<Startup>();
builder.Services.AddTransient<IGetFirstCommitByUsernameQuery, GetFirstCommitByUsernameQuery>();
builder.Services.AddTransient<IAzureDevOpsRepository, AzureDevOpsRepository>();

var app = builder.Build();
app.Run();

Console.WriteLine();
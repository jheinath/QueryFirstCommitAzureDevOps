using Adapters.AzureDevOps.Repositories;
using Application.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.AzureDevOps;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAzureDevopsAdapter(this IServiceCollection services)
    {
        return services
            .AddHttpClient()
            .AddTransient<IAzureDevOpsRepository, AzureDevOpsRepository>();
    }
}
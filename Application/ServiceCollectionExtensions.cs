﻿using Application.Queries;
using Application.Queries.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        return services.AddTransient<IGetFirstCommitsByUserEmailQuery, GetFirstCommitsByUserEmailQuery>();
    }
}
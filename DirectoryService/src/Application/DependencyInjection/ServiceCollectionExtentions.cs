using Application.Abstractions;
using Application.Locations.CreateLocation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjection;

public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<Guid, CreateLocationCommand>, CreateLocationHandler>();

        return services;
    }
}
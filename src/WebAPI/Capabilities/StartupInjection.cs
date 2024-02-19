using Application;
using Application.Interfaces;
using Application.Services;
using Clients;
using Infrastructure;
using WebAPI.BackgroundServices;

namespace WebAPI.Capabilities;

/// <summary>
/// Configure startup services 
/// </summary>
public static class StartupInjection
{
    /// <summary>
    /// Configure startup services 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static IServiceCollection ConfigureInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();

        services.AddApplication();
        services.AddClients();

        string dbConnectionString = configuration.GetConnectionString("PostgreConnection")
            ?? throw new ArgumentNullException("Postgre connection string not found");

        services.AddInfrastructure(dbConnectionString);

        services.AddHostedService<OrderCleaningBackgroundService>();

        return services;
    }
}

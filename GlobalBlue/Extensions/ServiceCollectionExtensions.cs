using GlobalBlue.Services;
using GlobalBlue.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBlue.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers application services with the dependency injection container.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the services to.</param>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculationService, CalculationService>();
        services.AddScoped<IValidator, CountryVatRateValidator>();
    }

    /// <summary>
    /// Adds API versioning to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add the API versioning to.</param>
    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });
    }
}

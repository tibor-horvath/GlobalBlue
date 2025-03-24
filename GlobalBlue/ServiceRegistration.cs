using GlobalBlue.Services;
using GlobalBlue.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlobalBlue;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculationService, CalculationService>();
        services.AddScoped<ICountryVatRateValidator, CountryVatRateValidator>();
    }

    public static void AddApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
        });
    }
}

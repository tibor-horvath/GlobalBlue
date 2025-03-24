using GlobalBlue.Services;
using GlobalBlue.Services.Interfaces;

namespace GlobalBlue;

public static class ServiceRegistration
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICalculationService, CalculationService>();
        services.AddScoped<ICountryVatRateValidator, CountryVatRateValidator>();
    }
}

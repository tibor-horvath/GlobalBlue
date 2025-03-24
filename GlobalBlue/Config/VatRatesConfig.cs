using GlobalBlue.Enums;

namespace GlobalBlue.Configuration;

/// <summary>
/// Configuration class for VAT rates per country.
/// </summary>
public static class VatRatesConfig
{
    /// <summary>
    /// Dictionary containing VAT rates for each country.
    /// </summary>
    public static readonly Dictionary<Country, HashSet<decimal>> VatRatesPerCountry = new()
    {
        { Country.AT, new HashSet<decimal> { 10m, 13m, 20m } }
    };
}

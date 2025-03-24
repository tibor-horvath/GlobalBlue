using GlobalBlue.Configuration;
using GlobalBlue.Enums;
using GlobalBlue.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Services;

public class CountryVatRateValidator : ICountryVatRateValidator
{
    /// <summary>
    /// Validates the VAT rate for a given country.
    /// </summary>
    /// <param name="country">The country for which the VAT rate is being validated.</param>
    /// <param name="vatRate">The VAT rate to validate.</param>
    /// <returns>
    /// A <see cref="ValidationResult"/> indicating whether the VAT rate is valid for the specified country.
    /// Returns <see cref="ValidationResult.Success"/> if the VAT rate is valid; otherwise, returns a <see cref="ValidationResult"/> with an error message.
    /// </returns>
    public ValidationResult? Validate(Country country, decimal vatRate)
    {
        if (!VatRatesConfig.VatRatesPerCountry.TryGetValue(country, out var validVatRates))
        {
            return new ValidationResult($"No VAT rates defined for country {country}.");
        }

        if (!validVatRates.Contains(vatRate))
        {
            return new ValidationResult($"Invalid VAT rate '{vatRate}' for country '{country}'. Valid rates are {string.Join(", ", validVatRates)}.");
        }

        return ValidationResult.Success;
    }
}

using GlobalBlue.Enums;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Services.Interfaces;

public interface ICountryVatRateValidator
{
    ValidationResult? Validate(Country country, decimal vatRate);
}

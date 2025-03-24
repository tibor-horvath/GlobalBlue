using GlobalBlue.Enums;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Validation.Interfaces;

public interface ICountryVatRateValidator
{
    ValidationResult? Validate(Country country, decimal vatRate);
}

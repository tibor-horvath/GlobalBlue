using GlobalBlue.Enums;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Services.Interfaces;

public interface IValidator
{
    ValidationResult? Validate(Country country, decimal vatRate);
}

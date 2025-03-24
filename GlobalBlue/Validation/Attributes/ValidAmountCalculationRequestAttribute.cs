using GlobalBlue.Dtos;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Validation.Attributes;

/// <summary>
/// Attribute to validate the AmountCalculationRequest object.
/// Ensures that exactly one of Net, Gross, or VAT amount is provided and that the provided amount is greater than zero.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter)]
public sealed class ValidAmountCalculationRequestAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not AmountCalculationRequest request)
        {
            return new ValidationResult("Invalid request object.");
        }

        bool hasNet = request.Net.HasValue;
        bool hasGross = request.Gross.HasValue;
        bool hasVat = request.Vat.HasValue;

        if ((hasNet ? 1 : 0) + (hasGross ? 1 : 0) + (hasVat ? 1 : 0) != 1)
        {
            return new ValidationResult("Exactly one of Net, Gross, or VAT amount must be provided.");
        }

        if (hasNet && request.Net <= 0 || hasGross && request.Gross <= 0 || hasVat && request.Vat <= 0)
        {
            return new ValidationResult("Amounts must be greater than zero.");
        }

        return ValidationResult.Success;
    }
}

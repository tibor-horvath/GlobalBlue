using GlobalBlue.Dtos;
using GlobalBlue.Enums;

namespace GlobalBlue.Extensions;

public static class AmountCalculationResultExtensions
{
    /// <summary>
    /// Converts an <see cref="AmountCalculationResult"/> to an <see cref="AmountCalculationResponse"/>.
    /// </summary>
    /// <param name="result">The result to convert.</param>
    /// <returns>An <see cref="AmountCalculationResponse"/> with rounded values.</returns>
    public static AmountCalculationResponse ToAmountCalculationResponse(this AmountCalculationResult result, Country country)
    {
        return new AmountCalculationResponse
        {
            Country = country,
            Net = Math.Round(result.Net, 2),
            Gross = Math.Round(result.Gross, 2),
            VatRatePercentage = result.VatRatePercentage,
            Vat = Math.Round(result.Vat, 2)
        };
    }
}

using GlobalBlue.Dtos;
using GlobalBlue.Services.Interfaces;

namespace GlobalBlue.Services;

public class CalculationService : ICalculationService
{
    /// <summary>
    /// Calculates the net, gross, and VAT amounts based on the provided request.
    /// </summary>
    /// <param name="request">The request containing the net, gross, or VAT amount and the VAT rate percentage.</param>
    /// <returns>An <see cref="AmountCalculationResult"/> containing the calculated net, gross, and VAT amounts.</returns>
    public AmountCalculationResult CalculateAmounts(AmountCalculationRequest request)
    {
        decimal net = 0, gross = 0, vat = 0;
        var vatRate = request.VatRatePercentage / 100;

        if (request.Net.HasValue)
        {
            net = request.Net.Value;
            vat = net * vatRate;
            gross = net + vat;
        }
        else if (request.Gross.HasValue)
        {
            gross = request.Gross.Value;
            vat = gross * vatRate / (1 + vatRate);
            net = gross - vat;
        }
        else if (request.Vat.HasValue)
        {
            vat = request.Vat.Value;
            net = vat / vatRate;
            gross = net + vat;
        }

        return new AmountCalculationResult
        {
            Net = net,
            Gross = gross,
            Vat = vat,
            VatRatePercentage = request.VatRatePercentage
        };
    }
}

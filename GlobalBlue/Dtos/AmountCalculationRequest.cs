namespace GlobalBlue.Dtos;

/// <summary>
/// Represents a request for amount calculation, including country, net amount, gross amount, VAT amount, and VAT rate percentage.
/// </summary>
public record AmountCalculationRequest
{
    /// <summary>
    /// Gets the net amount for the calculation.
    /// </summary>
    public decimal? Net { get; init; }

    /// <summary>
    /// Gets the gross amount for the calculation.
    /// </summary>
    public decimal? Gross { get; init; }

    /// <summary>
    /// Gets the VAT amount for the calculation.
    /// </summary>
    public decimal? Vat { get; init; }

    /// <summary>
    /// Gets the VAT rate percentage for the calculation.
    /// </summary>
    public required decimal VatRatePercentage { get; init; }
}

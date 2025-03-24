namespace GlobalBlue.Dtos;

/// <summary>
/// Represents the result of an amount calculation, including net, gross, and VAT amounts.
/// </summary>
public record AmountCalculationResult
{
    /// <summary>
    /// Gets or sets the net amount.
    /// </summary>
    public decimal Net { get; init; }

    /// <summary>
    /// Gets or sets the gross amount.
    /// </summary>
    public decimal Gross { get; init; }

    /// <summary>
    /// Gets or sets the VAT amount.
    /// </summary>
    public decimal Vat { get; init; }

    /// <summary>
    /// Gets or sets the VAT rate percentage.
    /// </summary>
    public decimal VatRatePercentage { get; init; }
}

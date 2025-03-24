using GlobalBlue.Enums;
using System.Text.Json.Serialization;

namespace GlobalBlue.Dtos;

/// <summary>
/// Represents the response for amount calculation.
/// </summary>
public record AmountCalculationResponse
{
    /// <summary>
    /// Gets the country associated with the amount calculation.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Country Country { get; init; }

    /// <summary>
    /// Gets the net amount.
    /// </summary>
    public decimal Net { get; init; }

    /// <summary>
    /// Gets the gross amount.
    /// </summary>
    public decimal Gross { get; init; }

    /// <summary>
    /// Gets the VAT amount.
    /// </summary>
    public decimal Vat { get; init; }

    /// <summary>
    /// Gets the VAT rate percentage.
    /// </summary>
    public required decimal VatRatePercentage { get; init; }
}

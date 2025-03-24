using GlobalBlue.Dtos;
using GlobalBlue.Services;
using Microsoft.Extensions.Logging.Abstractions;

namespace GlobalBlue.Tests.Services;
public class CalculationServiceTests
{
    private readonly CalculationService _calculationService;

    public CalculationServiceTests()
    {
        _calculationService = new CalculationService(new NullLogger<CalculationService>());
    }

    [Fact]
    public void CalculateAmounts_WithNetAmount_ReturnsCorrectAmounts()
    {
        // Arrange
        var request = new AmountCalculationRequest
        {
            Net = 100m,
            VatRatePercentage = 20m
        };

        // Act
        var result = _calculationService.CalculateAmounts(request);

        // Assert
        Assert.Equal(100m, result.Net);
        Assert.Equal(120m, result.Gross);
        Assert.Equal(20m, result.Vat);
        Assert.Equal(20m, result.VatRatePercentage);
    }

    [Fact]
    public void CalculateAmounts_WithGrossAmount_ReturnsCorrectAmounts()
    {
        // Arrange
        var request = new AmountCalculationRequest
        {
            Gross = 120m,
            VatRatePercentage = 20m
        };

        // Act
        var result = _calculationService.CalculateAmounts(request);

        // Assert
        Assert.Equal(100m, result.Net);
        Assert.Equal(120m, result.Gross);
        Assert.Equal(20m, result.Vat);
        Assert.Equal(20m, result.VatRatePercentage);
    }

    [Fact]
    public void CalculateAmounts_WithVatAmount_ReturnsCorrectAmounts()
    {
        // Arrange
        var request = new AmountCalculationRequest
        {
            Vat = 20m,
            VatRatePercentage = 20m
        };

        // Act
        var result = _calculationService.CalculateAmounts(request);

        // Assert
        Assert.Equal(100m, result.Net);
        Assert.Equal(120m, result.Gross);
        Assert.Equal(20m, result.Vat);
        Assert.Equal(20m, result.VatRatePercentage);
    }
}

using GlobalBlue.Dtos;
using GlobalBlue.Enums;
using GlobalBlue.Extensions;

namespace GlobalBlue.Tests.Extensions;
public class AmountCalculationResultExtensionsTests
{
    [Fact]
    public void ToAmountCalculationResponse_ShouldConvertCorrectly()
    {
        // Arrange
        var result = new AmountCalculationResult
        {
            Net = 100.1234m,
            Gross = 120.5678m,
            Vat = 20.1234m,
            VatRatePercentage = 20m
        };
        var country = Country.AT;

        // Act
        var response = result.ToAmountCalculationResponse(country);

        // Assert
        Assert.Equal(country, response.Country);
        Assert.Equal(100.12m, response.Net);
        Assert.Equal(120.57m, response.Gross);
        Assert.Equal(20.12m, response.Vat);
        Assert.Equal(20m, response.VatRatePercentage);
    }
}

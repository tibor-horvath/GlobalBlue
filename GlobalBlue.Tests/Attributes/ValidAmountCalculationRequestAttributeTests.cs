using GlobalBlue.Attributes;
using GlobalBlue.Dtos;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Tests.Attributes;
public class ValidAmountCalculationRequestAttributeTests
{
    private readonly ValidAmountCalculationRequestAttribute _attribute = new();

    [Fact]
    public void IsValid_ShouldReturnSuccess_WhenNetAmountIsProvided()
    {
        var request = new AmountCalculationRequest
        {
            Net = 100,
            VatRatePercentage = 20
        };

        var result = _attribute.GetValidationResult(request, new ValidationContext(request));

        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ShouldReturnSuccess_WhenGrossAmountIsProvided()
    {
        var request = new AmountCalculationRequest
        {
            Gross = 120,
            VatRatePercentage = 20
        };

        var result = _attribute.GetValidationResult(request, new ValidationContext(request));

        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ShouldReturnSuccess_WhenVatAmountIsProvided()
    {
        var request = new AmountCalculationRequest
        {
            Vat = 20,
            VatRatePercentage = 20
        };

        var result = _attribute.GetValidationResult(request, new ValidationContext(request));

        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void IsValid_ShouldReturnError_WhenNoAmountIsProvided()
    {
        var request = new AmountCalculationRequest
        {
            VatRatePercentage = 20
        };

        var result = _attribute.GetValidationResult(request, new ValidationContext(request));

        Assert.NotEqual(ValidationResult.Success, result);
        Assert.Equal("Exactly one of Net, Gross, or VAT amount must be provided.", result?.ErrorMessage);
    }

    [Fact]
    public void IsValid_ShouldReturnError_WhenMultipleAmountsAreProvided()
    {
        var request = new AmountCalculationRequest
        {
            Net = 100,
            Gross = 120,
            VatRatePercentage = 20
        };

        var result = _attribute.GetValidationResult(request, new ValidationContext(request));

        Assert.NotEqual(ValidationResult.Success, result);
        Assert.Equal("Exactly one of Net, Gross, or VAT amount must be provided.", result?.ErrorMessage);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void IsValid_ShouldReturnError_WhenNetAmountIsZeroOrNegative(decimal netAmount)
    {
        var request = new AmountCalculationRequest
        {
            Net = netAmount,
            VatRatePercentage = 20
        };

        var result = _attribute.GetValidationResult(request, new ValidationContext(request));

        Assert.NotEqual(ValidationResult.Success, result);
        Assert.Equal("Amounts must be greater than zero.", result?.ErrorMessage);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void IsValid_ShouldReturnError_WhenGrossAmountIsZeroOrNegative(decimal grossAmount)
    {
        var request = new AmountCalculationRequest
        {
            Gross = grossAmount,
            VatRatePercentage = 20
        };

        var result = _attribute.GetValidationResult(request, new ValidationContext(request));

        Assert.NotEqual(ValidationResult.Success, result);
        Assert.Equal("Amounts must be greater than zero.", result?.ErrorMessage);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-50)]
    public void IsValid_ShouldReturnError_WhenVatAmountIsZeroOrNegative(decimal vatAmount)
    {
        var request = new AmountCalculationRequest
        {
            Vat = vatAmount,
            VatRatePercentage = 20
        };

        var result = _attribute.GetValidationResult(request, new ValidationContext(request));

        Assert.NotEqual(ValidationResult.Success, result);
        Assert.Equal("Amounts must be greater than zero.", result?.ErrorMessage);
    }
}

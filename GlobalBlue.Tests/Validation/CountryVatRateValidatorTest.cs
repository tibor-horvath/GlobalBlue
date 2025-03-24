using GlobalBlue.Enums;
using GlobalBlue.Validation;
using Microsoft.Extensions.Logging.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Tests.Validation;
public class CountryVatRateValidatorTest
{
    private readonly CountryVatRateValidator _validator;

    public CountryVatRateValidatorTest()
    {
        _validator = new CountryVatRateValidator(new NullLogger<CountryVatRateValidator>());
    }

    [Fact]
    public void Validate_ShouldReturnSuccess_WhenVatRateIsValid()
    {
        var result = _validator.Validate(Country.AT, 20m);
        Assert.Equal(ValidationResult.Success, result);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenVatRateIsInvalid()
    {
        var result = _validator.Validate(Country.AT, 15m);
        Assert.NotEqual(ValidationResult.Success, result);
        Assert.Equal("Invalid VAT rate '15' for country 'AT'. Valid rates are 10, 13, 20.", result?.ErrorMessage);
    }

    [Fact]
    public void Validate_ShouldReturnError_WhenCountryHasNoVatRatesDefined()
    {
        var result = _validator.Validate((Country)999, 20m);
        Assert.NotEqual(ValidationResult.Success, result);
        Assert.Equal("No VAT rates defined for country 999.", result?.ErrorMessage);
    }
}

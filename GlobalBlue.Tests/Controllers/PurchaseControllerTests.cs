using GlobalBlue.Controllers;
using GlobalBlue.Dtos;
using GlobalBlue.Enums;
using GlobalBlue.Services.Interfaces;
using GlobalBlue.Validation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Tests.Controllers;
public class PurchaseControllerTests
{
    private readonly Mock<ICalculationService> _mockCalculationService;
    private readonly Mock<ICountryVatRateValidator> _mockCountryVatRateValidator;
    private readonly PurchaseController _controller;

    public PurchaseControllerTests()
    {
        _mockCalculationService = new Mock<ICalculationService>();
        _mockCountryVatRateValidator = new Mock<ICountryVatRateValidator>();
        _controller = new PurchaseController(_mockCalculationService.Object, _mockCountryVatRateValidator.Object, new NullLogger<PurchaseController>());
    }

    [Fact]
    public void CalculateAmounts_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var country = Country.AT;
        var request = new AmountCalculationRequest { VatRatePercentage = 20 };
        _mockCountryVatRateValidator
            .Setup(v => v.Validate(country, request.VatRatePercentage))
            .Returns(new ValidationResult("Invalid VAT rate"));

        // Act
        var result = _controller.CalculateAmounts(country, request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.NotNull(badRequestResult);
        Assert.NotNull(badRequestResult.Value);
        Assert.Equal("Invalid VAT rate", (badRequestResult.Value as ProblemDetails)!.Detail);
    }

    [Fact]
    public void CalculateAmounts_ReturnsOk_WhenValidationSucceeds()
    {
        // Arrange
        var country = Country.AT;
        var request = new AmountCalculationRequest { VatRatePercentage = 20 };
        var calculationResult = new AmountCalculationResult { Net = 100, Gross = 120, Vat = 20, VatRatePercentage = 20 };

        _mockCountryVatRateValidator
            .Setup(v => v.Validate(country, request.VatRatePercentage))
            .Returns(ValidationResult.Success);

        _mockCalculationService
            .Setup(s => s.CalculateAmounts(request))
            .Returns(calculationResult);

        // Act
        var result = _controller.CalculateAmounts(country, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<AmountCalculationResponse>(okResult.Value);
        Assert.Equal(100, response.Net);
        Assert.Equal(120, response.Gross);
        Assert.Equal(20, response.Vat);
        Assert.Equal(20, response.VatRatePercentage);
        Assert.Equal(Country.AT, response.Country);
    }
}

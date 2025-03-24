using GlobalBlue.Attributes;
using GlobalBlue.Dtos;
using GlobalBlue.Enums;
using GlobalBlue.Extensions;
using GlobalBlue.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Controllers;

[ApiController]
[Route("[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly ICalculationService _calculationService;
    private readonly ICountryVatRateValidator _countryVatRateValidator;

    public PurchaseController(
        ICalculationService calculationService,
        ICountryVatRateValidator countryVatRateValidator)
    {
        _calculationService = calculationService;
        _countryVatRateValidator = countryVatRateValidator;
    }

    /// <summary>
    /// Calculates the amounts based on the provided request.
    /// </summary>
    /// <param name="country">The country for which the calculation is being made.</param>
    /// <param name="request">The amount calculation request containing necessary data.</param>
    /// <returns>An IActionResult containing the calculation response or a bad request if the input is invalid.</returns>
    [HttpPost("calculate/{country:required}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult CalculateAmounts(
        [FromRoute] Country country,
        [FromBody, ValidAmountCalculationRequest] AmountCalculationRequest request)
    {
        var validationResult = _countryVatRateValidator.Validate(country, request.VatRatePercentage);

        if (validationResult != ValidationResult.Success)
        {
            return BadRequest(validationResult?.ErrorMessage);
        }

        var calculationResult = _calculationService.CalculateAmounts(request);

        return Ok(calculationResult.ToAmountCalculationResponse(country));
    }
}

using GlobalBlue.Attributes;
using GlobalBlue.Dtos;
using GlobalBlue.Enums;
using GlobalBlue.Extensions;
using GlobalBlue.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GlobalBlue.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class PurchaseController : ControllerBase
{
    private readonly ICalculationService _calculationService;
    private readonly IValidator _countryVatRateValidator;
    private readonly ILogger<PurchaseController> _logger;

    public PurchaseController(
        ICalculationService calculationService,
        IValidator countryVatRateValidator,
        ILogger<PurchaseController> logger)
    {
        _calculationService = calculationService;
        _countryVatRateValidator = countryVatRateValidator;
        _logger = logger;
    }

    /// <summary>
    /// Calculates the amounts based on the provided request.
    /// </summary>
    /// <param name="country">The country for which the calculation is being made.</param>
    /// <param name="request">The amount calculation request containing necessary data.</param>
    /// <returns>An IActionResult containing the calculation response or a bad request if the input is invalid.</returns>
    [HttpPost("calculate/{country:required}")]
    [MapToApiVersion("1")]
    [ProducesResponseType(typeof(AmountCalculationResponse), 200)]
    [ProducesResponseType(400)]
    public IActionResult CalculateAmounts(
        [FromRoute] Country country,
        [FromBody, ValidAmountCalculationRequest] AmountCalculationRequest request)
    {
        _logger.LogInformation("Received calculation request for country: {Country}", country);

        var validationResult = _countryVatRateValidator.Validate(country, request.VatRatePercentage);

        if (validationResult != ValidationResult.Success)
        {
            _logger.LogWarning("Validation failed for country: {Country} with error: {ErrorMessage}", country, validationResult?.ErrorMessage);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Detail = validationResult?.ErrorMessage
            });
        }

        var calculationResult = _calculationService.CalculateAmounts(request);
        _logger.LogInformation("Calculation successful for country: {Country}", country);

        return Ok(calculationResult.ToAmountCalculationResponse(country));
    }
}

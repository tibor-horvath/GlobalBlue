using GlobalBlue.Dtos;

namespace GlobalBlue.Services.Interfaces;

public interface ICalculationService
{
    AmountCalculationResult CalculateAmounts(AmountCalculationRequest request);
}

using CapitalClue.Frontend.Shared.Models;

namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IProfitCalculations
{
    (PropertyPredictionResult, StockPredictionResult) CompareHousePurchaseAndStock(float monthlyRent);

    PropertyPredictionResult PropertyPrediction();

    StockPredictionResult StockPrediction(float downpayment, float monthlyContribute);
}
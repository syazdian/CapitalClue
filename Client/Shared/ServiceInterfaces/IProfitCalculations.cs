using CapitalClue.Frontend.Shared.Models;

namespace CapitalClue.Frontend.Shared.ServiceInterfaces;

public interface IProfitCalculations
{
    (PropertyPredictionResult, StockPredictionResult) CompareHousePurchaseAndStock(float monthlyRent);

    PropertyPredictionResult PropertyPrediction(PropertyPredictionDto _propertyPredictionDto, PropertyPurchaseInfo _propertyPurchaseInfo);

    StockPredictionResult StockPrediction(float downpayment, float monthlyContribute);
}
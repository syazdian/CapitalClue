using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography.X509Certificates;

namespace CapitalClue.Frontend.Web.Services;

public class FetchData : IFetchData
{
    private readonly string baseAddress;
    private readonly HttpClient _httpClient;
    private IStateContainer _stateContainer;
    private readonly ILogger _logger;

    public FetchData(HttpClient httpClient, IStateContainer stateContainer, UrlKeeper url, ILogger<FetchData> logger)
    {
        _httpClient = httpClient;
        _stateContainer = stateContainer;
        baseAddress = url.BaseUrl;
        _logger = logger;
    }

    public async Task<PropertyPredictionDto> GetPropertyPredicionPercent(string city, string propertyType)
    {
        var url = $"{baseAddress}api/Property/PredictYearByYear/{city}/{propertyType}";

        var result = await _httpClient.GetFromJsonAsync<PropertyPredictionDto>(url);

        return result;
    }

    public async Task<StockPredictionDto> GetStockPredicionPercent(string StockName, string Currency = "US")
    {
        var url = $"{baseAddress}api/Stock/PredictYearByYear/{StockName}/{Currency}";

        var result = await _httpClient.GetFromJsonAsync<StockPredictionDto>(url);

        return result;
    }

    public async Task<StockPredictionDto> GetStockPredicionPercent()
    {
        string StockName = "S&P500";
        string Currency = "US";
        var url = $"{baseAddress}api/Stock/PredictYearByYear/{StockName}/{Currency}";

        var result = await _httpClient.GetFromJsonAsync<StockPredictionDto>(url);

        return result;
    }
}
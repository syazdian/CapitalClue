using Microsoft.Extensions.Logging;
using System;

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
}
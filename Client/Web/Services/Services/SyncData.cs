using CapitalClue.Frontend.Web.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CapitalClue.Frontend.Web.Services.Services;

public class SyncData : ISyncData
{
    private readonly string baseAddress;
    private readonly HttpClient _httpClient;
    private IStateContainer _stateContainer;
    private readonly ILogger _logger;

    public SyncData(HttpClient httpClient, IStateContainer stateContainer, UrlKeeper url, ILogger<SyncData> logger)
    {
        _httpClient = httpClient;
        _stateContainer = stateContainer;
        baseAddress = url.BaseUrl;
        _logger = logger;
    }

    public async Task SendPropertyDto(PropertyModelDto property)
    {
        var jsn = property.ToJson();
        //  await _httpClient.PostAsJsonAsync($"{baseAddress}api/Property/TrainAndCreateModel", property);
        await _httpClient.PostAsJsonAsync($"{baseAddress}api/Property/TrainAndCreateModel", property);
    }

    public async Task SendStockDto(StockModelDto stock)
    {
        var jsn = stock.ToJson();
        await _httpClient.PostAsJsonAsync($"{baseAddress}api/Stock/TrainAndCreateModel", stock);
    }
}
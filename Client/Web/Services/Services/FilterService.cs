using CapitalClue.Frontend.Shared.ServiceInterfaces;
using CapitalClue.Frontend.Web.Services.Database;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text.Json;

namespace CapitalClue.Frontend.Web.Services;

public class FilterService : IFilterService
{
    private readonly HttpClient _httpClient;

    private readonly string baseAddress;
    private readonly FilterItemsDisplay _filterItem;
    private readonly ILocalDbRepository _localdbRepository;

    public FilterService(HttpClient httpClient, FilterItemsDisplay filterItem, UrlKeeper url, ILocalDbRepository localDbRepository)
    {
        _httpClient = httpClient;
        _filterItem = filterItem;
        _localdbRepository = localDbRepository;

        baseAddress = url.BaseUrl;
    }

    public async Task<FilterItemsDisplay> GetFilterItems()
    {
        try
        {
             await _localdbRepository.GetStoreFromLocalDb();
            //_filterItem.StoreNumbers = stores;
          
            return _filterItem;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public string GetFilterJson()
    {
        string filterJson = string.Empty;
        return filterJson;
    }

    public async Task<string> GetHello()
    {
        try
        {
            //var url = $"{_httpClient.BaseAddress}api/FilterValue/GetHello";
            //var response = await _httpClient.GetAsync(url);
            var response = await _httpClient.GetAsync($"{baseAddress}api/FilterValue/GetHello");
            string res = await response.Content.ReadAsStringAsync();
            return res;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
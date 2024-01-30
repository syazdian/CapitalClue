using CapitalClue.Common.Models.Enums;
using CapitalClue.Web.Server.Data.Sqlserver;

namespace CapitalClue.Web.Server.Services;

public class CallApi
{
    //  private readonly TokenService _tokenService;
    private readonly ILogger _logger;

    private string BaseAddressUrl;// = "https://dev.api.staplescan.com/dev/bellservices/v1.0/mw/reconciliation/";

    public CallApi(BellRecContext bellContext, TokenService tokenService, IConfiguration configuration, ILogger<CallApi> logger)
    {
        //  _tokenService = tokenService;
        BaseAddressUrl = configuration["DetailApiBaseAddress"];
        _logger = logger;
    }

    public async Task<string> GetDetails(string title, string value)
    {
        try
        {
            // var accessToken = await _tokenService.GetAccessToken();
            HttpRequestMessage request = new();
            switch (title)
            {
                case "PhoneNumber":
                    request = new HttpRequestMessage(HttpMethod.Get, $"{BaseAddressUrl}orderByPhoneNumber/{value}");

                    break;

                case "OrderNumber":
                    request = new HttpRequestMessage(HttpMethod.Get, $"{BaseAddressUrl}orderByOrderNumber/{value}");
                    break;

                case "Imei":
                    request = new HttpRequestMessage(HttpMethod.Get, $"{BaseAddressUrl}orderBy/Imei/{value}");
                    break;

                case "Sim":
                    request = new HttpRequestMessage(HttpMethod.Get, $"{BaseAddressUrl}orderBySim/{value}");
                    break;
            }

            if (request is null)
            {
                return "{\"Error\" : \"Details based on Order Number is not available\" } ";
            }

            // request.Headers.Add("Authorization", "Bearer" + " " + accessToken);
            string responseContent = string.Empty;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.SendAsync(request);
                responseContent = await response.Content.ReadAsStringAsync();
            }
            if (responseContent.Contains("Internal Server Error"))
            {
                return "{\"Error\" : \"Internal Server Error\" } ";
            }
            var result = new JsonManipulating().JsonManipulatingService(responseContent);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetDetails : {ex.Message}");
            throw;
        }
    }
}
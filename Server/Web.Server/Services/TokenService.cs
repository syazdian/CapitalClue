using CapitalClue.Common.Utilities;
using CapitalClue.Web.Server.Model;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace CapitalClue.Web.Server.Services;

public class TokenService
{
    private string auth_token_url;
    private string client_id;
    private string client_secret;
    private string scope;
    private string grant_type;
    private string accessToken;
    private string token_name = "ItemAPIToken";
    private long expire;

    private readonly ILogger _logger;
    private readonly IMemoryCache _memoryCache;
    private IConfiguration _configuration;

    public TokenService(IConfiguration configuration, IMemoryCache cache, ILogger<TokenService> logger)
    {
        _configuration = configuration;
        _memoryCache = cache;

        auth_token_url = _configuration.GetValue<string>("ApiToken:token_url");
        client_id = _configuration.GetValue<string>("ApiToken:client_id");
        client_secret = _configuration.GetValue<string>("ApiToken:client_secret");
        scope = _configuration.GetValue<string>("ApiToken:scope");
        grant_type = _configuration.GetValue<string>("ApiToken:grant_type");
        _logger = logger;
    }

    public async Task<string> GetAccessToken()
    {
        try
        {
            if (!GetAccessTokenFromCache())
            {
                await GetNewTokent();
            }

            return accessToken;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetAccessToken : {ex.Message}");
            throw;
        }
    }

    private Boolean GetAccessTokenFromCache()
    {
        try
        {
            AppToken appToken;
            if (_memoryCache.TryGetValue(token_name, out appToken) && (appToken != null) && (appToken.ExpireDT > DateTime.Now))
            {
                accessToken = appToken.Token;
                return true;
            }
            else
            {
                _memoryCache.Remove(token_name); //remove expired cache token
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetAccessTokenFromCache : {ex.Message}");
            throw;
        }
    }

    private async Task<bool> GetNewTokent()
    {
        try
        {
            Boolean lResult = false;
            accessToken = string.Empty;
            expire = 0;
            var form = new Dictionary<string, string>
                {
                    {"client_id", client_id},
                    {"client_secret", client_secret},
                    {"grant_type", grant_type},
                    {"scope", scope}
                };
            var content = new FormUrlEncodedContent(form);
            // var request = new HttpRequestMessage(HttpMethod.Post, auth_token_url) { Content = content };
            using (var client = new HttpClient())
            {
                HttpResponseMessage tokenresponse = await client.PostAsync(auth_token_url, content);
                if (tokenresponse != null)
                {
                    string responseJsonContent = tokenresponse.Content.ReadAsStringAsync().Result;
                    var result = responseJsonContent.JsonDeserialize<AccessTokenModel>();
                    accessToken = result.AccessToken;
                    expire = result.ExpiresIn;

                    var appToken = new AppToken();
                    appToken.TokenName = token_name;
                    appToken.Token = accessToken;
                    appToken.ExpireIn = expire;
                    appToken.ExpireDT = System.DateTime.Now.AddSeconds(expire - 500);
                    appToken.GenereateDT = System.DateTime.Now;
                    _memoryCache.Set<AppToken>(token_name, appToken);

                    lResult = true;
                }
                else
                {
                    lResult = false;
                }
            }
            return lResult;
        }
        catch (Exception ex)
        {
            _logger.LogError($"GetNewTokent : {ex.Message}");

            return false;
        }
    }
}
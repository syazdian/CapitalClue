using System.Text.Json.Serialization;

namespace CapitalClue.Web.Server.Model
{
    public class Token
    {
    }

    public class AccessTokenModel
    {
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }

        [JsonPropertyName("ext_expires_in")]
        public long ExtExpiresIn { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
    }

    public class AppToken
    {
        public string TokenName { get; set; }
        public DateTime GenereateDT { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDT { get; set; }
        public long ExpireIn { get; set; }
    }
}
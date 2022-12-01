using Framework.Properties.Constants;

namespace Framework.Api.Request.Clubware
{
    public class ClubwareAuthorizationRequest
    {
        public ClubwareAuthorizationRequest()
        {

        }
        public ClubwareAuthorizationRequest(string appId, string secret, string refreshToken)
        {
            AppID = appId;
            Secret = secret;
            RefreshToken = refreshToken;
        }

        public string AppID { get; set; } = ApiServerConfiguration.ConfigurationManager["Configuration:Clubware:AppID"];
        public string Secret { get; set; } = ApiServerConfiguration.ConfigurationManager["Configuration:Clubware:Secret"];
        public string RefreshToken { get; set; } = ApiServerConfiguration.ConfigurationManager["Configuration:Clubware:RefreshToken"];

    }
}

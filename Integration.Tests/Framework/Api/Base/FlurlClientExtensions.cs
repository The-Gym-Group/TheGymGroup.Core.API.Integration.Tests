using Flurl.Http;
using Framework.Api.Request.Clubware;
using Framework.Api.Response.Clubware;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Framework.Api.Base
{
    public static class FlurlClientExtensions
    {
        public static IFlurlClient AddBaseHeaders(this IFlurlClient flurlClient)
        {
            return flurlClient.WithHeaders(new
            {
                Content = "application/json",

            });
        }

        /// <summary>
        /// Configure a flurlClient for use with Clubware, after setting the site path.
        /// </summary>
        public static async Task<IFlurlClient> ConfigureForClubware(this IFlurlClient flurlClient, string baseUrl=null)
        {
            flurlClient = SetBaseUrl(flurlClient, baseUrl);
            //set base headers
            flurlClient = flurlClient = AddBaseConfHeaders(flurlClient);
            flurlClient = await SetAccessToken(flurlClient);

            return flurlClient;
        }

        private static IFlurlClient AddBaseConfHeaders(IFlurlClient flurlClient)
        {

            return flurlClient.WithHeaders(new 
            {
                Accept = "application/json; charset=utf-8",
                Content_Type = "application/json",
            });
        }

        private static IFlurlClient SetBaseUrl(IFlurlClient flurlClient, string baseUrl)
        {
            flurlClient.BaseUrl = baseUrl ?? flurlClient.BaseUrl;
            return flurlClient;
        }

        public static IFlurlClient ConfigureForTGGCoreApi(this IFlurlClient flurlClient, string baseUrl = null)
        {
            flurlClient = SetBaseUrl(flurlClient, baseUrl);
            flurlClient = AddBaseConfHeaders(flurlClient);

            flurlClient = flurlClient.WithHeaders(new
            {
                Cache_Control = "no-cache",
                Ocp_Apim_Subscription_Key = "f4767d1bcc1d4e08a422399c4b604b2c",
                version = "v1.0"
            });

            return flurlClient;//flurlClient.WithAdminAuthorization();
        }

        public static IFlurlClient WithAdminAuthorization(this IFlurlClient flurlClient)
        {
            flurlClient = SetXApiHeaders(flurlClient, "TggApi", "fucmEawQ7XXAHUPgByIfT2eRL50x5fMQDFsK2ykmNVXFASclxTCUBVeFQmvd");
            return flurlClient;
        }

        public static IFlurlClient SetXApiHeaders(this IFlurlClient flurlClient, string userAuthValue, string keyAuthValue)
        {
            flurlClient = flurlClient.WithHeader("X-Api-User", userAuthValue);
            flurlClient = flurlClient.WithHeader("X-Api-Key", keyAuthValue);
            return flurlClient;
        }

        /// <summary>
        /// Get and set the access token header from Clubware to the flurlClient
        /// </summary>
        /// <exception cref="BadHttpRequestException"></exception>
        private static async Task<IFlurlClient> SetAccessToken(IFlurlClient flurlClient)
        {
            string url = $"{flurlClient.BaseUrl}/authorisation/getaccesstoken";
            var content = new ClubwareAuthorizationRequest();
            try
            {
                var response = await url.PostJsonAsync(content);
                if (response != null)
                {
                    var authorizationData = await response.GetJsonAsync<ClubwareAccessTokenResponseData>();
                    flurlClient = flurlClient.WithHeader("AccessToken", authorizationData.TokenValue);
                }
                else
                {
                    throw new BadHttpRequestException("Did not receive a response from Clubware when trying to reach authorization.");
                }
            }
            catch (Exception any)
            {
                Console.WriteLine(any.Message);
                Console.WriteLine(any.StackTrace);
                throw;
            }

            return flurlClient;
        }
    }
}

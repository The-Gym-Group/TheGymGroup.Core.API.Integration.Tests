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
            flurlClient = flurlClient.WithHeaders(new
            {
                Connection = "keep-alive",
                Accept = "application/json",
                Content = "application/json",
            })
                .WithHeader("Accept-Encoding", "gzip, deflate, br")
                .WithHeader("Content-Type", "application/x-www-form-urlencoded");
            return flurlClient;
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

            return flurlClient.WithHeader("Authorization", "Basic e3tCYXNpY0F1dGhVc2VybmFtZX19Ont7QmFzaWNBdXRoUGFzc3dvcmR9fQ==");
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

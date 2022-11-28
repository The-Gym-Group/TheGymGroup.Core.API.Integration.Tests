using Flurl.Http;

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
    }
}

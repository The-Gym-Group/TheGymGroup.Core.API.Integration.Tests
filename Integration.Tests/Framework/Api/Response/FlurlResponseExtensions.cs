using Flurl.Http;

namespace Framework.Api.Response
{
    public static class FlurlResponseExtensions
    {
        public static async Task<T> GetDataAsync<T>(this FlurlResponse self) where T: class
        {
            return (await self.GetJsonAsync()) as T;
        }

        public static async Task<IEnumerable<T>> GetListDataAsync<T>(this FlurlResponse self) where T:class
        {
            return (await self.GetJsonAsync()).ToList() as List<T>;
        }
    }
}

using Framework.Api.Response;

namespace Framework.Api.Base
{
    public interface IApiHelper<T> where T : class
    {
        public Task<CoreApiResponse<T>> GetSingle(string endpointPath, params string[] queryParameters);
        public Task<CoreApiResponse<IEnumerable<T>>> GetMultiple(string endpointPath, params string[] queryParameters);
        public Task<CoreApiResponse<T>> Post(string endpointPath, object request);
    }
}

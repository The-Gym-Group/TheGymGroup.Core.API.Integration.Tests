using Framework.Api.Response;

namespace Framework.Api.Base
{
    public interface IApiHelper<T> where T : class
    {
        public CoreApiResponse<T> GetSingle(string endpointPath, params string[] queryParameters);
        public CoreApiResponse<IEnumerable<T>> GetMultiple(string endpointPath, params string[] queryParameters);
        public CoreApiResponse<T> Post(string endpointPath, object request);
    }
}

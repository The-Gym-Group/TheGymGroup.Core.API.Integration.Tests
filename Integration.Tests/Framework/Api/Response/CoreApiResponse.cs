using System.Net;

namespace Framework.Api.Response
{
    public class CoreApiResponse<T> where T : class
    {
        public bool Success { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}

namespace Framework.Api.Response.CoreAPI
{
    public class CoreApiResponse <T> where T: class
    {
        public T Data { get; set; }
        public bool Success { get; set; }
    }
}

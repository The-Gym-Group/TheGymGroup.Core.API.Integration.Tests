namespace Framework.Properties.Constants
{
    public static class ApiServerConfiguration
    {
        public static ConfigurationManager ConfigurationManager { get; set; } 
        public static string ROOT_URL { get; set; }
        public static string ENVIRONMENT { get; set; } = "sit";   //local, sit or pat
        public static string CoreAPI_URL { get=>ENVIRONMENT=="local"?ROOT_URL:$"https://{ENVIRONMENT}-api-proxy.tggdev.com"; }
        public static string CLUBWARE_URL { get => $"https://api-tgg-{ENVIRONMENT}.clubware.co.uk:2433"; }
    }
}

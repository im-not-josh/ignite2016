namespace Xtrade.Shared
{
    using Newtonsoft.Json;

    public static class Configuration
    {
        private const string BaseUrl = "https://api.asb.co.nz/public";
        private const string ApiVersionUrl = "/v1/";

        public const int TimeoutInterval = 10000;

        public static string MobileApiUrl
        {
            get { return BaseUrl + ApiVersionUrl; }
        }

        public static JsonSerializerSettings JsonSettings
        {
            get
            {
                return new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Local };
            }
        }
    }
}

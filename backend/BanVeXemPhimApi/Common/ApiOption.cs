namespace BanVeXemPhimApi.Common
{
    public class ApiOption
    {
        public string coreDB { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
        public string BaseUrl { get; set; }
    }
}

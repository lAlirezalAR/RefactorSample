
namespace Utilities.Framework.Config
{

    public class UrlsConfig
    {
        public string Users { get; set; } = default!;

        public string Contacts { get; set; }

        public string Lines { get; set; }

        public string BaseInfo { get; set; }

        public string Payments { get; set; }
        public string SMSRequestManagement { get; set; }
        public string SMSStatusReport { get; set; }
        public string SMSTransmissions { get; set; }

        public string GrpcUser { get; set; }
        public string GrpcBaseInfo { get; set; }
        public string GrpcLine { get; set; }


        public class BaseInfoOperations
        {
            public static string GenerateToken() => "/api/v1/TokenStore";
            public static string CheckTokenIsValid() => "/api/v1/CheckTokenIsValid";
        }
    }
}

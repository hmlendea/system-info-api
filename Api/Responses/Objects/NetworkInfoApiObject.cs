using Newtonsoft.Json;
using NuciSecurity.HMAC;

namespace SystemInfoApi.Api.Responses.Objects
{
    public class NetworkInfoApiObject
    {
        [HmacOrder(1)]
        [JsonProperty("public_ip")]
        public string PublicIpAddress { get; set; }

        [HmacOrder(2)]
        [JsonProperty("hostname")]
        public string Hostname { get; set; }
    }
}

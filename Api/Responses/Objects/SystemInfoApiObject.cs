using Newtonsoft.Json;
using NuciSecurity.HMAC;

namespace SystemInfoApi.Api.Responses.Objects
{
    public class SystemInfoApiObject
    {
        [HmacOrder(1)]
        [JsonProperty("os")]
        public string OperatingSystem { get; set; }

        [HmacOrder(2)]
        [JsonProperty("kernel")]
        public string Kernel { get; set; }

        [HmacOrder(3)]
        [JsonProperty("arch")]
        public string Architecture { get; set; }

        [HmacOrder(4)]
        [JsonProperty("hostname")]
        public string Hostname { get; set; }

        [HmacOrder(5)]
        [JsonProperty("uptime")]
        public int Uptime { get; set; }
    }
}

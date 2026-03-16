using Newtonsoft.Json;
using NuciSecurity.HMAC;

namespace SystemInfoApi.Api.Responses.Objects
{
    public class RegionInfoApiObject
    {
        [HmacOrder(1)]
        [JsonProperty("system_time")]
        public string SystemTime { get; set; }

        [HmacOrder(2)]
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }
    }
}

using Newtonsoft.Json;
using NuciAPI.Responses;
using NuciSecurity.HMAC;
using SystemInfoApi.Api.Responses.Objects;

namespace SystemInfoApi.Api.Responses
{
    public class GetSystemInfoResponse : NuciApiSuccessResponse
    {
        [HmacOrder(1)]
        [JsonProperty("system")]
        public SystemInfoApiObject SystemInfo { get; set; }

        [HmacOrder(2)]
        [JsonProperty("network")]
        public NetworkInfoApiObject NetworkInfo { get; set; }

        [HmacOrder(3)]
        [JsonProperty("region")]
        public RegionalInfoApiObject RegionalInfo { get; set; }
    }
}

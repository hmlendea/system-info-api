namespace SystemInfoApi.Configuration
{
    public sealed class SecuritySettings
    {
        public string ApiKey { get; set; }

        public string HmacSigningKey { get; set; }
    }
}

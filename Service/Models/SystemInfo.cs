namespace SystemInfoApi.Service.Models
{
    public sealed class SystemInfo
    {
        public string OperatingSystem { get; set; }

        public string Kernel { get; set; }

        public string Architecture { get; set; }

        public string Hostname { get; set; }

        public int Uptime { get; set; }
    }
}

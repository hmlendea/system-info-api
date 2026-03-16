using NuciLog.Core;

namespace SystemInfoApi.Logging
{
    public sealed class MyLogInfoKey : LogInfoKey
    {
        MyLogInfoKey(string name) : base(name) { }

        public static LogInfoKey Architecture => new MyLogInfoKey(nameof(Architecture));
        public static LogInfoKey Hostname => new MyLogInfoKey(nameof(Hostname));
        public static LogInfoKey Kernel => new MyLogInfoKey(nameof(Kernel));
        public static LogInfoKey LocalIpAddress => new MyLogInfoKey(nameof(LocalIpAddress));
        public static LogInfoKey OperatingSystem => new MyLogInfoKey(nameof(OperatingSystem));
        public static LogInfoKey PublicIpAddress => new MyLogInfoKey(nameof(PublicIpAddress));
        public static LogInfoKey SystemTime => new MyLogInfoKey(nameof(SystemTime));
        public static LogInfoKey TimeZone => new MyLogInfoKey(nameof(TimeZone));
        public static LogInfoKey Uptime => new MyLogInfoKey(nameof(Uptime));
    }
}

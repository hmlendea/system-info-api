using NuciLog.Core;

namespace SystemInfoApi.Logging
{
    public sealed class MyOperation : Operation
    {
        MyOperation(string name) : base(name) { }

        public static Operation GetRegionalInfo => new MyOperation(nameof(GetRegionalInfo));
        public static Operation GetSystemInfo => new MyOperation(nameof(GetSystemInfo));
        public static Operation GetNetworkInfo => new MyOperation(nameof(GetNetworkInfo));
    }
}

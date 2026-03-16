using NuciLog.Core;

namespace SystemInfoApi.Logging
{
    public sealed class MyOperation : Operation
    {
        MyOperation(string name) : base(name) { }

        public static Operation GetSystemInfo => new MyOperation(nameof(GetSystemInfo));
    }
}

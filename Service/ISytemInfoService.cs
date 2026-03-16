using SystemInfoApi.Service.Models;

namespace SystemInfoApi.Service
{
    public interface ISystemInfoService
    {
        SystemInfo GetSystemInfo();

        NetworkInfo GetNetworkInfo();

        RegionInfo GetRegionInfo();
    }
}

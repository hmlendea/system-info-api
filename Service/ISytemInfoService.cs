using SystemInfoApi.Service.Models;

namespace SystemInfoApi.Service
{
    public interface ISystemInfoService
    {
        SystemInfo GetSystemInfo();
    }
}

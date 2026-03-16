using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using NuciLog.Core;
using NuciWeb.HTTP;
using SystemInfoApi.Logging;
using SystemInfoApi.Service.Models;

namespace SystemInfoApi.Service
{
    public sealed class SystemInfoService(ILogger logger) : ISystemInfoService
    {
        public SystemInfo GetSystemInfo()
        {
            logger.Info(
                MyOperation.GetSystemInfo,
                OperationStatus.Started);

            SystemInfo systemInfo = new()
            {
                OperatingSystem = GetOperatingSystemName(),
                Kernel = GetKernelVersion(),
                Architecture = GetOperatingSystemArchitecture(),
                Hostname = Environment.MachineName,
                Uptime = (int)Environment.TickCount64 / 1000
            };

            logger.Info(
                MyOperation.GetSystemInfo,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.OperatingSystem, systemInfo.OperatingSystem),
                new LogInfo(MyLogInfoKey.Kernel, systemInfo.Kernel),
                new LogInfo(MyLogInfoKey.Architecture, systemInfo.Architecture),
                new LogInfo(MyLogInfoKey.Hostname, systemInfo.Hostname),
                new LogInfo(MyLogInfoKey.Uptime, systemInfo.Uptime));

            return systemInfo;
        }

        public NetworkInfo GetNetworkInfo()
        {
            logger.Info(
                MyOperation.GetNetworkInfo,
                OperationStatus.Started);

            NetworkInfo networkInfo = new()
            {
                PublicIpAddress = NetworkUtils.GetPublicIpAddress(),
                Hostname = Environment.MachineName
            };

            logger.Info(
                MyOperation.GetNetworkInfo,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.PublicIpAddress, networkInfo.PublicIpAddress),
                new LogInfo(MyLogInfoKey.Hostname, networkInfo.Hostname));

            return networkInfo;
        }

        public RegionInfo GetRegionInfo()
        {
            logger.Info(
                MyOperation.GetRegionInfo,
                OperationStatus.Started);

            RegionInfo regionInfo = new()
            {
                SystemTime = DateTimeOffset.Now.ToString("o"),
                TimeZone = TimeZoneInfo.Local.ToString()
            };

            logger.Info(
                MyOperation.GetRegionInfo,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.SystemTime, regionInfo.SystemTime),
                new LogInfo(MyLogInfoKey.TimeZone, regionInfo.TimeZone));

            return regionInfo;
        }

        static string GetOperatingSystemArchitecture()
            => RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "x64",
                Architecture.X86 => "x86",
                Architecture.Arm64 => "arm64",
                Architecture.Arm => "arm",
                _ => RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant()
            };

        static string GetOperatingSystemName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                const string osReleasePath = "/etc/os-release";

                if (File.Exists(osReleasePath))
                {
                    foreach (string line in File
                        .ReadLines(osReleasePath)
                        .Where(line => line.StartsWith("PRETTY_NAME=", StringComparison.Ordinal)))
                    {
                        string prettyName = line["PRETTY_NAME=".Length..].Trim().Trim('"');

                        if (!string.IsNullOrWhiteSpace(prettyName))
                        {
                            return prettyName;
                        }
                    }
                }
            }

            return RuntimeInformation.OSDescription;
        }

        static string GetKernelVersion()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                const string kernelReleasePath = "/proc/sys/kernel/osrelease";

                if (File.Exists(kernelReleasePath))
                {
                    string kernelRelease = File.ReadAllText(kernelReleasePath).Trim();

                    if (!string.IsNullOrWhiteSpace(kernelRelease))
                    {
                        return kernelRelease;
                    }
                }
            }

            return RuntimeInformation.OSDescription;
        }
    }
}

using System;
using System.IO;
using System.Runtime.InteropServices;
using NuciLog.Core;
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
                Architecture = RuntimeInformation.OSArchitecture switch
                {
                    Architecture.X64 => "x64",
                    Architecture.X86 => "x86",
                    Architecture.Arm64 => "arm64",
                    Architecture.Arm => "arm",
                    _ => RuntimeInformation.OSArchitecture.ToString().ToLowerInvariant()
                },
                Hostname = Environment.MachineName,
                Uptime = (int)Environment.TickCount64 / 1000
            };

            logger.Debug(
                MyOperation.GetSystemInfo,
                OperationStatus.Success,
                new LogInfo(MyLogInfoKey.OperatingSystem, systemInfo.OperatingSystem),
                new LogInfo(MyLogInfoKey.Kernel, systemInfo.Kernel),
                new LogInfo(MyLogInfoKey.Architecture, systemInfo.Architecture),
                new LogInfo(MyLogInfoKey.Hostname, systemInfo.Hostname),
                new LogInfo(MyLogInfoKey.Uptime, systemInfo.Uptime));

            return systemInfo;
        }

        static string GetOperatingSystemName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                const string osReleasePath = "/etc/os-release";

                if (File.Exists(osReleasePath))
                {
                    foreach (string line in File.ReadLines(osReleasePath))
                    {
                        if (!line.StartsWith("PRETTY_NAME=", StringComparison.Ordinal))
                        {
                            continue;
                        }

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

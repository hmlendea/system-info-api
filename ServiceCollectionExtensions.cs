using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NuciLog;
using NuciLog.Configuration;
using NuciLog.Core;
using SystemInfoApi.Configuration;
using SystemInfoApi.Service;

namespace SystemInfoApi
{
    public static class ServiceCollectionExtensions
    {
        static SecuritySettings securitySettings;
        static NuciLoggerSettings loggingSettings;

        public static IServiceCollection AddConfigurations(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            securitySettings = new SecuritySettings();
            loggingSettings = new NuciLoggerSettings();

            configuration.Bind(nameof(SecuritySettings), securitySettings);
            configuration.Bind(nameof(NuciLoggerSettings), loggingSettings);

            services.AddSingleton(securitySettings);
            services.AddSingleton(loggingSettings);

            return services;
        }

        public static IServiceCollection AddCustomServices(
            this IServiceCollection services) => services
                .AddSingleton<ISystemInfoService, SystemInfoService>()
                .AddSingleton<ILogger, NuciLogger>();
    }
}

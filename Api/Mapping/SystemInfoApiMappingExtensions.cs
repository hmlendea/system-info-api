using System.Collections.Generic;
using System.Linq;
using SystemInfoApi.Api.Responses.Objects;
using SystemInfoApi.Service.Models;

namespace SystemInfoApi.Api.Mapping
{
    /// <summary>
    /// SystemInfo mapping extensions for converting between api objects and domain models.
    /// </summary>
    static class SystemInfoApiMappingExtensions
    {
        /// <summary>
        /// Converts the domain model into a data object.
        /// </summary>
        /// <returns>The data object.</returns>
        /// <param name="domainModel">The domain model.</param>
        internal static SystemInfoApiObject ToApiObject(
            this SystemInfo domainModel) => new()
        {
            OperatingSystem = domainModel.OperatingSystem,
            Kernel = domainModel.Kernel,
            Architecture = domainModel.Architecture,
            Hostname = domainModel.Hostname,
            Uptime = domainModel.Uptime
        };

        /// <summary>
        /// Converts the domain models into api objects.
        /// </summary>
        /// <returns>The api objects.</returns>
        /// <param name="domainModels">The domain models.</param>
        internal static IEnumerable<SystemInfoApiObject> ToApiObjects(
            this IEnumerable<SystemInfo> domainModels)
            => domainModels.Select(domainModel => domainModel.ToApiObject());
    }
}

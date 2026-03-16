using System.Collections.Generic;
using System.Linq;
using SystemInfoApi.Api.Responses.Objects;
using SystemInfoApi.Service.Models;

namespace SystemInfoApi.Api.Mapping
{
    /// <summary>
    /// NetworkInfo mapping extensions for converting between api objects and domain models.
    /// </summary>
    static class NetworkInfoApiMappingExtensions
    {
        /// <summary>
        /// Converts the domain model into a data object.
        /// </summary>
        /// <returns>The data object.</returns>
        /// <param name="domainModel">The domain model.</param>
        internal static NetworkInfoApiObject ToApiObject(
            this NetworkInfo domainModel) => new()
        {
            PublicIpAddress = domainModel.PublicIpAddress,
            Hostname = domainModel.Hostname
        };

        /// <summary>
        /// Converts the domain models into api objects.
        /// </summary>
        /// <returns>The api objects.</returns>
        /// <param name="domainModels">The domain models.</param>
        internal static IEnumerable<NetworkInfoApiObject> ToApiObjects(
            this IEnumerable<NetworkInfo> domainModels)
            => domainModels.Select(domainModel => domainModel.ToApiObject());
    }
}

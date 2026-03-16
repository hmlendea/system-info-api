using System.Collections.Generic;
using System.Linq;
using SystemInfoApi.Api.Responses.Objects;
using SystemInfoApi.Service.Models;

namespace SystemInfoApi.Api.Mapping
{
    /// <summary>
    /// RegionInfo mapping extensions for converting between api objects and domain models.
    /// </summary>
    static class RegionInfoApiMappingExtensions
    {
        /// <summary>
        /// Converts the domain model into a data object.
        /// </summary>
        /// <returns>The data object.</returns>
        /// <param name="domainModel">The domain model.</param>
        internal static RegionInfoApiObject ToApiObject(
            this RegionInfo domainModel) => new()
        {
            SystemTime = domainModel.SystemTime,
            TimeZone = domainModel.TimeZone
        };

        /// <summary>
        /// Converts the domain models into api objects.
        /// </summary>
        /// <returns>The api objects.</returns>
        /// <param name="domainModels">The domain models.</param>
        internal static IEnumerable<RegionInfoApiObject> ToApiObjects(
            this IEnumerable<RegionInfo> domainModels)
            => domainModels.Select(domainModel => domainModel.ToApiObject());
    }
}

using System.Collections.Generic;
using System.Linq;
using SystemInfoApi.Api.Responses.Objects;
using SystemInfoApi.Service.Models;

namespace SystemInfoApi.Api.Mapping
{
    /// <summary>
    /// RegionalInfo mapping extensions for converting between api objects and domain models.
    /// </summary>
    static class RegionalInfoApiMappingExtensions
    {
        /// <summary>
        /// Converts the domain model into a data object.
        /// </summary>
        /// <returns>The data object.</returns>
        /// <param name="domainModel">The domain model.</param>
        internal static RegionalInfoApiObject ToApiObject(
            this RegionalInfo domainModel) => new()
        {
            SystemTime = domainModel.SystemTime,
            TimeZone = domainModel.TimeZone
        };

        /// <summary>
        /// Converts the domain models into api objects.
        /// </summary>
        /// <returns>The api objects.</returns>
        /// <param name="domainModels">The domain models.</param>
        internal static IEnumerable<RegionalInfoApiObject> ToApiObjects(
            this IEnumerable<RegionalInfo> domainModels)
            => domainModels.Select(domainModel => domainModel.ToApiObject());
    }
}

using System.Runtime.Serialization;

namespace Kyrio.Services.Serviceability
{
    /// <summary>
    /// Response from cable provider with service status for requested location.
    /// </summary>
    [DataContract]
    public class ServiceabilityResult
    {
        /// <summary>
        /// This field will be present if the provider has assigned a unique location identifier (aka housekey) for the address.
        /// </summary>
        [DataMember(Name = "location_id")]
        public string LocationId { get; set; }

        /// <summary>
        /// This field will contain the value ‘residential’ or ‘business’ if the provider characterizes the location.
        /// Otherwise a value of ‘unknown’ will be returned.
        /// </summary>
        [DataMember(Name = "location_type")]
        public LocationType LocationType { get; set; }

        /// <summary>
        /// A 4-digit identifier will be returned for each provider.
        /// </summary>
        [DataMember(Name = "provider_id")]
        public string ProviderId { get; set; }

        /// <summary>
        /// Company name associated with the provider id.
        /// </summary>
        [DataMember(Name = "provider")]
        public string Provider { get; set; }

        /// <summary>
        /// Service status for the location (site).
        /// </summary>
        [DataMember(Name = "site_status")]
        public SiteStatus SiteStatus { get; set; }
    }
}

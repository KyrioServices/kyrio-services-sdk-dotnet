using System.Runtime.Serialization;

namespace Kyrio.Services.Serviceability
{
    [DataContract]
    public class ServiceabilityResult
    {
        [DataMember(Name = "location_id")]
        public string LocationId { get; set; }

        [DataMember(Name = "location_type")]
        public LocationType LocationType { get; set; }

        [DataMember(Name = "provider_id")]
        public string ProviderId { get; set; }

        [DataMember(Name = "provider")]
        public string Provider { get; set; }

        [DataMember(Name = "site_status")]
        public SiteStatus SiteStatus { get; set; }
    }
}

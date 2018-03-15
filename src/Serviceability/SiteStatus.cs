using System.Runtime.Serialization;

namespace Kyrio.Services.Serviceability
{
    /// <summary>
    /// Determines service status of a site (location).
    /// </summary>
    [DataContract]
    public enum SiteStatus
    {
        /// <summary>
        /// Service is not available
        /// </summary>
        [EnumMember(Value = "none")]
        None,

        /// <summary>
        /// Indicates that one or more cable services are currently available at the address
        /// </summary>
        [EnumMember(Value = "on_net")]
        OnNet,

        /// <summary>
        /// Indicates the MSO has previously surveyed the site and determined it cannot be served
        /// </summary>
        [EnumMember(Value = "off_net")]
        OffNet,

        /// <summary>
        /// Indicates the address is near existing cable infrastructure and can likely be served at a reasonable cost/effort
        /// </summary>
        [EnumMember(Value = "near_net")]
        NearNet,

        /// <summary>
        /// Indicates the MSO must conduct additional analysis to determine if the site can be served
        /// </summary>
        [EnumMember(Value = "survey_req")]
        SurveyRequired,

        /// <summary>
        /// Indicates that the MSO serves the general area such as the 5-digit ZIP code or locations
        /// within a configurable distance (such as 60-feet or 200-feet). 
        /// The MSO should be contacted for additional information about the serviceability of a location identified as ‘proximity’.
        /// </summary>
        [EnumMember(Value = "proximity")]
        Proximity
    }
}

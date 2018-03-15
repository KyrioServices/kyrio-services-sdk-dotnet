using System.Runtime.Serialization;

namespace Kyrio.Services.Serviceability
{
    /// <summary>
    /// Defines types for servicable locations
    /// </summary>
    [DataContract]
    public enum LocationType
    {
        /// <summary>
        /// Location does not exist or not listed
        /// </summary>
        [EnumMember(Value = "none")]
        None,

        /// <summary>
        /// Location type is unknown
        /// </summary>
        [EnumMember(Value = "unknown")]
        Unknown,

        /// <summary>
        /// Location is a business property
        /// </summary>
        [EnumMember(Value = "business")]
        Business,

        /// <summary>
        /// Location is a residential property
        /// </summary>
        [EnumMember(Value = "residential")]
        Residential
    }
}

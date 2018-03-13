using System.Runtime.Serialization;

namespace Kyrio.Services.Serviceability
{
    [DataContract]
    public enum LocationType
    {
        [EnumMember(Value = "none")]
        None,

        [EnumMember(Value = "unknown")]
        Unknown,

        [EnumMember(Value = "business")]
        Business,

        [EnumMember(Value = "residential")]
        Residential
    }
}

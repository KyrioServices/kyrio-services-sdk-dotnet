using System.Runtime.Serialization;

namespace Kyrio.Services.Serviceability
{
    [DataContract]
    public enum SiteStatus
    {
        [EnumMember(Value = "none")]
        None,

        [EnumMember(Value = "on_net")]
        OnNet,

        [EnumMember(Value = "off_net")]
        OffNet,

        [EnumMember(Value = "near_net")]
        NearNet,

        [EnumMember(Value = "survey_req")]
        SurveyRequired,

        [EnumMember(Value = "proximity")]
        Proximity
    }
}

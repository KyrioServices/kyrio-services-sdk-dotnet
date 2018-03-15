using System.Runtime.Serialization;

namespace Kyrio.Services.Shared
{
    [DataContract]
    public class Address
    {
        [DataMember(Name = "line1")]
        public string Line1 { get; set; }

        [DataMember(Name = "line2")]
        public string Line2 { get; set; }

        [DataMember(Name = "city")]
        public string City { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }

        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }
    }
}

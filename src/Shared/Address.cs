using System.Runtime.Serialization;

namespace Kyrio.Services.Shared
{
    /// <summary>
    /// Location postal address.
    /// </summary>
    [DataContract]
    public class Address
    {
        /// <summary>
        /// Street number, pre-directional, street name, suffix, post-directional
        /// Examples:
        /// 123 N Main St
        /// 234 Michigan Ave SW
        /// </summary>
        [DataMember(Name = "line1")]
        public string Line1 { get; set; }

        /// <summary>
        /// Secondary address line such as Apt, Suite or Lot
        /// </summary>
        [DataMember(Name = "line2")]
        public string Line2 { get; set; }

        /// <summary>
        /// City or town name
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// For US addresses, usethe standard 2-character state abbreviation
        /// </summary>
        [DataMember(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// For US addresses, use the 5-digit ZIP code
        /// </summary>
        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Use ‘US’ to indicate US addresses.  If the argument is omitted, ‘US’ will be assumed.
        /// Refer to ISO 3166 Country Code Standardfor non-US addresses.
        /// </summary>
        [DataMember(Name = "country_code")]
        public string CountryCode { get; set; }
    }
}

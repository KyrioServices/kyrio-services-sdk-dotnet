using System.Runtime.Serialization;

namespace Kyrio.Services.Shared
{
    /// <summary>
    /// Cable service provider (also known as MSO)
    /// </summary>
    [DataContract]
    public class Provider
    {
        /// <summary>
        /// A 4-digit identifier will be returned for each provider. 
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Company name associated with the provider id.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}

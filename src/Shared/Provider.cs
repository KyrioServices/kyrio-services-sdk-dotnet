using System.Runtime.Serialization;

namespace Kyrio.Services.Shared
{
    [DataContract]
    public class Provider
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}

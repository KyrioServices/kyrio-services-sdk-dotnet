using System;
using System.Runtime.Serialization;

namespace Kyrio.Services.Shared
{
    [DataContract]
    public class KyrioException: ApplicationException
    {
        public KyrioException(string message)
            : base(message)
        { }

        public KyrioException(string code, int status, string message)
            : base(message)
        {
            Code = code;
            Status = status;
        }

        public KyrioException(string code, int status, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
            Status = status;
        }

        public string Code { get; set; }
        public int Status { get; set; }
    }
}

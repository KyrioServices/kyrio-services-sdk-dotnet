using System;

namespace Kyrio.Services
{
    /// <summary>
    /// Exception that represents error responses from Kyrio Online services
    /// </summary>
    public class KyrioException: ApplicationException
    {
        /// <summary>
        /// Creates Kyrio exception with message.
        /// </summary>
        /// <param name="message">Textual description of the error</param>
        public KyrioException(string message)
            : base(message)
        { }

        /// <summary>
        /// Creates Kyrio exception with detail information.
        /// </summary>
        /// <param name="code">Unique code that defines error type</param>
        /// <param name="status">HTTP response code returned by server</param>
        /// <param name="message">Textual descriotion of the error</param>
        public KyrioException(string code, int status, string message)
            : base(message)
        {
            Code = code;
            Status = status;
        }

        /// <summary>
        /// Creates Kyrio exception with detail information and original exception.
        /// </summary>
        /// <param name="code">Unique code that defines error type</param>
        /// <param name="status">HTTP response code returned by server</param>
        /// <param name="message">Textual descriotion of the error</param>
        /// <param name="innerException">Original exception</param>
        public KyrioException(string code, int status, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
            Status = status;
        }

        /// <summary>
        /// Unique code that defines error type.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// HTTP response code returned by server.
        /// </summary>
        public int Status { get; }
    }
}

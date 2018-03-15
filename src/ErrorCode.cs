namespace Kyrio.Services
{
    /// <summary>
    ///  Unique codes that define Kyrio error types.
    /// </summary>
    public  class ErrorCode
    {
        /// <summary>
        /// No connection to Kyrio servers.
        /// </summary>
        public const string NO_CONNECTION = "NO_CONNECTION";

        /// <summary>
        /// Kyrio was not able to authorize client by provided ID.
        /// </summary>
        public const string UNAUTHORIZED = "UNAUTHORIZED";

        /// <summary>
        /// Request contained invalid parameters.
        /// </summary>
        public const string BAD_REQUEST = "BAD_REQUEST";

        /// <summary>
        /// Cause of error was unknown.
        /// </summary>
        public const string UNKNOWN = "UNKNOWN";

        /// <summary>
        /// Internal server error.
        /// </summary>
        public const string INTERNAL = "INTERNAL";

        /// <summary>
        /// Response to server or MSOs failed after timeout.
        /// </summary>
        public const string TIMEOUT = "TIMEOUT";

        /// <summary>
        /// Server is temporary unavailable due to maintenance.
        /// </summary>
        public const string MAINTENANCE = "MAINTENANCE";
    }
}

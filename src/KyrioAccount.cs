using Kyrio.Services.Serviceability;
using System;
using System.Text.RegularExpressions;

namespace Kyrio.Services
{
    /// <summary>
    /// Account to Kyrio Online Services. It is used to set common connection properties and create clients to access individual services.
    /// </summary>
    public class KyrioAccount
    {
        private const string PROD_SERVER_URL = "https://api.kyrioconnectionsuite.com";
        private const string QA_SERVER_URL = "https://api.qa.kyrioconnectionsuite.com";

        private static readonly Regex CLIENT_ID_REGEX = new Regex(@"^\d{6}$", RegexOptions.IgnoreCase);
        private static readonly Regex SERVER_URL_REGEX = new Regex(@"^(https?:\/\/)?[\w -]+(\.[\w-]+)+\.?(:\d+)?$", RegexOptions.IgnoreCase);

        private string _clientId;
        private string _serverUrl;
        private bool _enableTestLocal = false;
        private bool _enableTestMock = false;
        private bool _enableTestError = false;
        private bool _enableQaEnvironment = false;

        /// <summary>
        /// Default account constructor
        /// </summary>
        public KyrioAccount() { }

        /// <summary>
        /// Identifier to confirm client who accesses the API.
        /// Usually it is set as 6 digit number.
        /// </summary>
        public string ClientId
        {
            get { return _clientId; }
            set
            {
                if (value == "")
                    throw new ArgumentException("ClientId cannot be empty");
                if (!KyrioAccount.CLIENT_ID_REGEX.IsMatch(value))
                    throw new ArgumentException("ClientId must be 6 digits long");

                _clientId = value;
            }
        }

        /// <summary>
        /// Base url to connect to Kyrio servers.
        /// It is an optional property. It is set automatically based on EnableQaEnvironment.
        /// But user is able to override it.
        /// </summary>
        public string ServerUrl
        {
            get
            {
                if (_serverUrl != null)
                    return _serverUrl;
                else if (_enableQaEnvironment)
                    return KyrioAccount.QA_SERVER_URL;
                else
                    return KyrioAccount.PROD_SERVER_URL;
            }
            set
            {
                if (value == "")
                    throw new ArgumentException("ServerUrl cannot be empty");
                if (!KyrioAccount.SERVER_URL_REGEX.IsMatch(value))
                    throw new ArgumentException("ServerUrl must be set as https://<host>[:<port>]");

                _serverUrl = value;
            }
        }

        /// <summary>
        /// Enables local test calls and returns random responses.
        /// This allows to avoid roundtrips to Kyrio servers and incurring changes for API use.
        /// The responses are delayed for 1.5 sec to make them more realistic.
        /// This property works together with EnableTestError.
        /// </summary>
        public bool EnableTestLocal
        {
            get { return _enableTestLocal; }
            set { _enableTestLocal = value; }
        }

        /// <summary>
        /// Enables remote test calls and returns random responses.
        /// With this property enabled client makes calls to Kyrio servers without changes for API use.
        /// This property works together with EnableTestError.
        /// </summary>
        public bool EnableTestMock
        {
            get { return _enableTestMock; }
            set { _enableTestMock = value; }
        }

        /// <summary>
        /// Enables random errors while making test calls.
        /// The errors simulate Internal (500) or Timeout (504) responses with 1% probability.
        /// This property works together with EnableTestLock and EnableTestMock.
        /// </summary>
        public bool EnableTestError
        {
            get { return _enableTestError; }
            set { _enableTestError = value; }
        }

        /// <summary>
        /// Enables calls to QA environment and sets default ServerUrl to QA servers.
        /// In the future this property can be deprecated.
        /// </summary>
        public bool EnableQaEnvironment
        {
            get { return _enableQaEnvironment; }
            set { _enableQaEnvironment = value; }
        }

        /// <summary>
        /// Creates client to access Kyrio Serviceability API.
        /// </summary>
        /// <returns>Client to access Kyrio Serviceability API.</returns>
        public ServiceabilityClient CreateServiceabilityClient() {
            return new ServiceabilityClient(this);
        }    
    }
}

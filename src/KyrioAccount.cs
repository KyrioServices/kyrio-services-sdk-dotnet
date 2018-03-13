using Kyrio.Services.Serviceability;
using System;
using System.Text.RegularExpressions;

namespace Kyrio.Services
{
    public class KyrioAccount
    {
        private const string PROD_SERVER_URL = "https://api.kyrioconnectionsuite.com";
        private const string QA_SERVER_URL = "https://api.qa.kyrioconnectionsuite.com";

        private static readonly Regex CLIENT_ID_REGEX = new Regex(@"\d{6}", RegexOptions.IgnoreCase);
        private static readonly Regex SERVER_URL_REGEX = new Regex(@"(https?:\/\/)?[\w -]+(\.[\w-]+)+\.?(:\d+)?", RegexOptions.IgnoreCase);

        private string _clientId;
        private string _serverUrl = "http://localhost:7277";
        private bool _enableTestLocal = false;
        private bool _enableTestMock = false;
        private bool _enableTestError = false;
        private bool _enableQaEnvironment = false;

        public KyrioAccount() { }

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

        public bool EnableTestLocal
        {
            get { return _enableTestLocal; }
            set { _enableTestLocal = value; }
        }

        public bool EnableTestMock
        {
            get { return _enableTestMock; }
            set { _enableTestMock = value; }
        }

        public bool EnableTestError
        {
            get { return _enableTestError; }
            set { _enableTestError = value; }
        }

        public bool EnableQaEnvironment
        {
            get { return _enableQaEnvironment; }
            set { _enableQaEnvironment = value; }
        }

        public ServiceabilityClient CreateServiceabilityClient() {
            return new ServiceabilityClient(this);
        }    
    }
}

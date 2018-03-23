using Kyrio.Services.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kyrio.Services.Serviceability
{
    /// <summary>
    /// Client to access Kyrio Serviceability API
    /// </summary>
    public class ServiceabilityClient: KyrioRestClient
    {
        private const string BASE_ROUTE = "/business/api/v1";

        /// <summary>
        /// Creates cliemt instance
        /// </summary>
        /// <param name="account">A Kyrio account this client is related to</param>
        public ServiceabilityClient(KyrioAccount account)
            : base(account)
        { }

        /// <summary>
        /// Determines cable providers that serve location specified by it's postal address.
        /// The method supports incomplete addresses: addressLine1 and postalCode
        /// or addressLine1, city and state.
        /// </summary>
        /// <param name="addressLine1">Street number, pre-directional, street name, suffix, post-directional</param>
        /// <param name="addressLine2">Secondary address line such as Apt, Suite or Lot</param>
        /// <param name="city">City or town name</param>
        /// <param name="state">For US addresses, usethe standard 2-character state abbreviation</param>
        /// <param name="postalCode">For US addresses, use the 5-digit ZIP code</param>
        /// <param name="countryCode">Use ‘US’ to indicate US addresses.  If the argument is omitted, ‘US’ will be assumed.
        /// Refer to ISO 3166 Country Code Standardfor non-US addresses.</param>
        /// <returns>Array of serviceability results from cable providers</returns>
        public async Task<ServiceabilityResult[]> DetermineBusinessServiceabilityAsync(
            string addressLine1, string addressLine2, string city,
            string state, string postalCode, string countryCode)
        {
            var address = new Address
            {
                Line1 = addressLine1,
                Line2 = addressLine2,
                City = city,
                State = state,
                PostalCode = postalCode,
                CountryCode = countryCode
            };

            return await DetermineBusinessServiceabilityForAddressAsync(address);
        }

        /// <summary>
        /// Determines cable providers that serve location specified by it's postal address.
        /// </summary>
        /// <param name="address">Location postal address</param>
        /// <returns>Array of serviceability results from cable providers</returns>
        public async Task<ServiceabilityResult[]> DetermineBusinessServiceabilityForAddressAsync(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");

            // For local testing return mock without calling server
            if (_account.EnableTestLocal)
                return await MockDetermineBusinessServiceabilityAsync(address);

            // Prepare invocation parameters
            var route = BASE_ROUTE + "/serviceability";
            var parameters = new Dictionary<string, object> {
                { "address_line1", address.Line1 },
                { "address_line2", address.Line2 },
                { "city", address.City },
                { "state", address.State },
                { "postal_code", address.PostalCode },
                { "country_code", address.CountryCode }
            };

            // Invoke operation on the server
            return await InvokeAsync<ServiceabilityResult[]>("GET", route, parameters, null);
        }

        /// <summary>
        /// Generates random test serviceability response.
        /// </summary>
        /// <param name="address">Location postal address</param>
        /// <returns>Array of serviceability results from cable providers</returns>
        private async Task<ServiceabilityResult[]> MockDetermineBusinessServiceabilityAsync(Address address)
        {
            await Task.Delay(1500);

            // Simulate random errors
            if (this._account.EnableTestError && RandomData.Chance(1, 100))
                throw RandomData.NextError();

            // Generate random results
            var resultCount = RandomData.NextInteger(0, 2);
            var results  = new List<ServiceabilityResult>();
            for (var index = 0; index < resultCount; index++)
            {
                var provider = RandomData.NextProvider();
                var result = new ServiceabilityResult
                {
                    LocationId = RandomData.NextInteger(99999).ToString(),
                    LocationType = RandomData.Pick<LocationType>(new LocationType[] {
                        LocationType.Unknown, LocationType.Residential, LocationType.Business
                    }),
                    ProviderId = provider.Id,
                    Provider = provider.Name,
                    SiteStatus = RandomData.Pick<SiteStatus>(new SiteStatus[] {
                        SiteStatus.OnNet, SiteStatus.OffNet, SiteStatus.NearNet,
                        SiteStatus.SurveyRequired, SiteStatus.Proximity
                    })
                };
                results.Add(result);
            }

            return results.ToArray();
        }

    }
}

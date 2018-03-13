using Kyrio.Services.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kyrio.Services.Serviceability
{
    public class ServiceabilityClient: KyrioRestClient
    {
        private const string BASE_ROUTE = "/api/v1";

        public ServiceabilityClient(KyrioAccount account)
            : base(account)
        { }

        public async Task<ServiceabilityResult[]> DetermineBusinessServiceabilityAsync(
            string addressLine1, string addressLine2, string city,
            string state, string postalCode, string country)
        {
            var address = new Address
            {
                Line1 = addressLine1,
                Line2 = addressLine2,
                City = city,
                State = state,
                PostalCode = postalCode,
                Country = country
            };

            return await DetermineBusinessServiceabilityForAddressAsync(address);
        }

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
                { "country", address.Country }
            };

            // Invoke operation on the server
            return await InvokeAsync<ServiceabilityResult[]>("GET", route, parameters, null);
        }

        private async Task<ServiceabilityResult[]> MockDetermineBusinessServiceabilityAsync(Address address)
        {
            await Task.Delay(1500);

            // Simulate random errors
            if (this._account.EnableTestError && RandomData.Chance(1, 10))
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

using Xunit;

namespace Kyrio.Services.Serviceability
{
    public class ServiceabilityClientTest
    {
        [Fact]
        public void TestDetermineBusinessServiceabilityMock()
        {
            var account = new KyrioAccount();
            account.ClientId = "999999";
            account.EnableTestLocal = true;

            var client = account.CreateServiceabilityClient();
            var results = client.DetermineBusinessServiceabilityAsync(
                "858 Coal Creek Circle", null, "Louisville", "CO", "80027", "US"
            ).Result;

            Assert.NotNull(results);
        }

        [Fact]
        public void TestDetermineBusinessServiceability()
        {
            var account = new KyrioAccount();
            account.ClientId = "999999";
            account.EnableTestMock = true;
            account.EnableTestError = false;

            var client = account.CreateServiceabilityClient();
            var results = client.DetermineBusinessServiceabilityAsync(
                "858 Coal Creek Circle", null, "Louisville", "CO", "80027", "US"
            ).Result;

            Assert.NotNull(results);
        }
    }
}

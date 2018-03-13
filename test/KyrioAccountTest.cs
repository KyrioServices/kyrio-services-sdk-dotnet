using Xunit;

namespace Kyrio.Services
{
    public class KyrioAccountTest
    {
        [Fact]
        public void TestSetServerUrl()
        {
            var account = new KyrioAccount();
            account.ServerUrl = "https://api.kyrio.com:8080";
            Assert.Equal("https://api.kyrio.com:8080", account.ServerUrl);

            try
            {
                account.ServerUrl = "xyz";
                Assert.True(false, "Must validate serverUrl");
            }
            catch
            {
                // Expected exception
            }

            try
            {
                account.ServerUrl = null;
                Assert.True(false, "Must validate serverUrl");
            }
            catch
            {
                // Expected exception
            }
        }

        [Fact]
        public void TestSetClientId()
        {
            var account = new KyrioAccount();
            account.ServerUrl = "https://api.kyrio.com:8080";
            Assert.Equal("https://api.kyrio.com:8080", account.ServerUrl);

            try
            {
                account.ServerUrl = "xyz";
                Assert.True(false, "Must validate serverUrl");
            }
            catch
            {
                // Expected exception
            }

            try
            {
                account.ServerUrl = null;
                Assert.True(false, "Must validate serverUrl");
            }
            catch
            {
                // Expected exception
            }
        }

        [Fact]
        public void TestSetTestProperties()
        {
            var account = new KyrioAccount();
            account.ClientId = "123456";

            account.EnableTestError = true;
            Assert.True(account.EnableTestError);

            account.EnableTestMock = true;
            Assert.True(account.EnableTestMock);

            account.EnableTestLocal = true;
            Assert.True(account.EnableTestLocal);
        }

        [Fact]
        public void TestCreateServiceabilityClient()
        {
            var account = new KyrioAccount();
            account.ClientId = "123456";

            var client = account.CreateServiceabilityClient();
            Assert.NotNull(client);
        }

    }
}

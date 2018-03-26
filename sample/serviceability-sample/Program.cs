using Kyrio.Services;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        MainAsync().Wait();
    }

    static async Task MainAsync()
    {
        var account = new KyrioAccount();
        account.ClientId = "999999";

        var client = account.CreateServiceabilityClient();
        try
        {
            var result = await client.DetermineBusinessServiceabilityAsync(
                "858 Coal Creek Circle", null, "Louisville", "CO", "80027", "US");

            var json = JsonConvert.SerializeObject(result);
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            // If you use Wait() or Result() rather than await,
            // The Exception will found in the InnerException of ex
            Console.WriteLine("Failed to call serviceability API");
            Console.WriteLine(ex.Message);
        }
    }
}

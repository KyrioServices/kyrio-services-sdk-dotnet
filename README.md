# .NET SDK to Kyrio Online Services

This SDK provides full access to Kyrio Online Services public API.

## Install

```bash
Package-Install Kyrio.Services
```

## Use

```dotnet
using Kyrio.Services;
using Newtonsoft.Json;
using System;

class Program
{
    static void Main(string[] args)
    {
        var account = new KyrioAccount();
        account.ClientId = "999999";

        var client = account.CreateServiceabilityClient();
        try
        {
            var result = client.DetermineBusinessServiceabilityAsync(
                "858 Coal Creek Circle", null, "Louisville", "CO", "80027", "US"
            ).Result;

            var json = JsonConvert.SerializeObject(result);
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to call serviceability API");
            Console.WriteLine(ex.Message);
        }
    }
}
```

## References

- [API Documentation](https://rawgit.com/KyrioServices/kyrio-services-sdk-dotnet/master/doc/api/index.html)

## License

This SDK is distributed under MIT license and free to use for all Kyrio clients.
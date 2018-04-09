See [KyrioService](https://github.com/KyrioServices/KyrioServices) for a language-agnostic description of the API

# .NET SDK to Kyrio Services

This SDK provides full access to Kyrio Services public API.

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
                "858 Coal Creek Circle", null, "Louisville", "CO", "80027", "US"
            );

            var json = JsonConvert.SerializeObject(result);
            Console.WriteLine(json);
        }
        catch (Exception ex)
        {
			// If you use Wait() or Result() rather than await,
			// The relevant exception will be the InnerException of ex

            Console.WriteLine("Failed to call serviceability API");
            Console.WriteLine(ex.Message);
        }
    }
}
```

### Testing flags

You can configure the client to return local test data before you start calling the API. When this is on,
all provider names will appear with names like "Local Test Cable Provider A".

```dotnet
account.EnableTestLocal = true
```

For testing purposes, you can instruct the API to return random test data
```dotnet
account.EnableTestMock = true
```

If you would like to test how your code handles error conditions, turn on the EnableTestError flag.
This works with both local data and data returned from the API when EnableTestMock is enabled.
```dotnet
account.EnableTestError = true
```

To reach Kyrio's QA endpoint rather than the production end-point, end able the EnableQaEnvironment flag
```dotnet
account.EnableQaEnvironment = true
```

## References

- [API Documentation](https://rawgit.com/KyrioServices/kyrio-services-sdk-dotnet/master/doc/api/index.html)
- [Development Guide](https://github.com/KyrioServices/kyrio-services-sdk-dotnet/blob/master/doc/Development.md)

## License

This SDK is distributed under MIT license and free to use for all Kyrio clients.

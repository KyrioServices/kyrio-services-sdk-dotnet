# Development Guide <br/> Kyrio Online Services SDK for .NET

This document provides high-level instructions on how to build and test the microservice.

* [Environment Setup](#setup)
* [Installing](#install)
* [Building](#build)
* [Testing](#test)
* [Release](#release)

## <a name="setup"></a> Environment Setup

This is a .NET project with multiple build targets for .NET full and .NET core frameworks. 
To be able to develop and test it you need to install the following components:
- Visual Studio 2017 Professional or Community Edition: https://www.visualstudio.com 
- .NET Core SDK with Visual Studio extentions: https://www.microsoft.com/net/core 

To work with GitHub code repository you need to install Git from: https://git-scm.com/downloads

If you plan to generate sourcecode documentation you need install Doxigen from: http://www.stack.nl/~dimitri/doxygen/download.html

## <a name="install"></a> Installing

After your environment is ready you can check out source code from the Github repository:
```bash
git clone git@github.com:KyrioServices/kyrio-services-sdk-dotnet
```

## <a name="build"></a> Building

Build the project from inside the Visual Studio. Alternatively you can use dotnet
to restore dependencies and compile source code:

```bash
dotnet restore src/src.csproj
dotnet build src/src.csproj
```

To generate source code documentation open Doxygen application and load project configuration
from Doxyfile file located at in the root folder. Then check destination folders and click run button.

## <a name="test"></a> Testing

The tests can be executed inside the Visual Studio. If you prefer to use command line
use the following commands:

```bash
dotnet restore test/test.csproj
dotnet test test/test.csproj
```

## <a name="release"></a> Release

Detail description of the NuGet release publishing procedure 
is described at http://docs.nuget.org/ndocs/create-packages/publish-a-package

Before publishing a new release you shall register on NuGet site and get you API Key.
Then register your API Key as:

```bash
nuget setApiKey Your-API-Key
```

Update release notes in CHANGELOG. Update version number and release details in nuspec file.
After that compile in Release (!!) configuration and test the project.
Then create a nuget package:

```bash
nuget pack kyrio-services-sdk-dotnet.nuspec
```

Publish the package on nuget global repository

```bash
nuget push Kyrio.Services.XXX.nupkg -Source https://www.nuget.org/api/v2/package
```

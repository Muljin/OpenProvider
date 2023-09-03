# OpenProvider
OpenProvider C# client library.

This library is provided as is. No support is provided

This library is built using NSwag from the OpenAPI specifications available at https://docs.openprovider.com/swagger.json

## Installation
To install in an Asp.net project, add the following services:

```
var openProviderConfig = config.GetSection("OpenProvider").Get<OpenProviderConfiguration>();
services.AddSingleton(openProviderConfig);
services.AddScoped<IOpenProviderApiClient, OpenProviderApiClient>();
builder.Services.AddHttpClient<IOpenProviderHttpClient, OpenProviderHttpClient>();

```

## Build
To generate the library, after install thge NSwag command line tool, run:
```nswag run```

in the OpenProvider\OpenProvider directory

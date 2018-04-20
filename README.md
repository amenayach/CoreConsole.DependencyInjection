### To enable JSON configuration we need the following:

Nuget packages:
* Microsoft.Extensions.Configuration
* Microsoft.Extensions.Configuration.FileExtensions
* Microsoft.Extensions.Configuration.Json

Add appsettings.json files “Copy to Output Directory” property should also be set to “Copy if newer” so that the application is able to access it when published.

```csharp
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

// ...

static void Main(string[] args)
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

    IConfigurationRoot configuration = builder.Build();

    Console.WriteLine(configuration["<JsonSection>:<PropertyName>"));
}
```

### To enable DI we need the following:

Nuget packages:
* Microsoft.Extensions.DependencyInjection
* Microsoft.Extensions.Logging (optional)
* Microsoft.Extensions.Logging.Console (optional)

```csharp
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

// ...

//DI
var serviceProvider = new ServiceCollection()
.AddLogging()
.AddSingleton<IDataScrapper, HttpDataScrapper>()
.BuildServiceProvider();

//configure console logging
serviceProvider
    .GetService<ILoggerFactory>()
    .AddConsole(LogLevel.Debug);

var logger = serviceProvider.GetService<ILoggerFactory>()
    .CreateLogger<Program>();
logger.LogDebug("Starting application");

logger.LogInformation("Url: " + url);

//do the actual work here
var scrapper = serviceProvider.GetService<IDataScrapper>();
var data = scrapper.GetData(url);
```

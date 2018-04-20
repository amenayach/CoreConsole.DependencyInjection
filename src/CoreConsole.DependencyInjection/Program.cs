namespace CoreConsole.DependencyInjection
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    
    class Program
    {
        static void Main(string[] args)
        {
            //Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var url = configuration["Test:Url"];

            //DI
            var serviceProvider = new ServiceCollection()
            .AddLogging()
            .AddSingleton<IDataScraper, HttpDataScraper>()
            .BuildServiceProvider();

            //configure console logging
            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Debug);

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>();
            logger.LogDebug("Starting application");

            logger.LogInformation($"Url: {url}");

            //do the actual work here
            var scrapper = serviceProvider.GetService<IDataScraper>();
            var data = scrapper.GetData(url);

            Console.WriteLine(data);

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}

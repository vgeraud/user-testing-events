using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Workleap.Configuration.ConfigurationStore;

// using Workleap.Configuration.Store;

namespace Workleap.UserTesting.Api;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureConfigurationStore()
            /* .ConfigureLogging((context, logging) =>
            {
                logging.AddObservability(context.Configuration);
            })*/
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}

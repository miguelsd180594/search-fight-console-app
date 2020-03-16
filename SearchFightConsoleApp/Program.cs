using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFightConsoleApp.Extensions;
using Serilog;
using System;

namespace SearchFightConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = BuildConfiguration();
            EnableLogging(configuration);

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection, configuration);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            try
            {
                serviceProvider.GetService<SearchFightConsoleApp>().Run(args);
            }
            catch (Exception exception)
            {
                var errorGuid = Guid.NewGuid();
                Log.Error("Error Id: {Id}, \r\n Error Information: {Error}", errorGuid, exception);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        #region Private Methods

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCustomApiClients(configuration)
                .AddCustomServices();
        }

        private static IConfiguration BuildConfiguration()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            return config;
        }

        private static void EnableLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .WriteTo.Console()
                .CreateLogger();
        }

        #endregion

    }
}

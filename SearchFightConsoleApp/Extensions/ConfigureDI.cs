using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFightConsoleApp.IServices;
using SearchFightConsoleApp.Models.Responses;
using SearchFightConsoleApp.Services;
using SearchFightConsoleApp.Services.ApiClients;
using SearchFightConsoleApp.Services.ApiClients.Interfaces;

namespace SearchFightConsoleApp.Extensions
{
    public static class ConfigureDI
    {
        private const string GOOGLE_SEARCH_ENGINE = "SearchEngines:0";
        private const string BING_SEARCH_ENGINE = "SearchEngines:1";

        public static IServiceCollection AddCustomApiClients(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddTransient<ISearchEngineApiClient<GoogleResponse>>(opt => new GoogleSearchEngineApiClient(configuration.GetSection($"{GOOGLE_SEARCH_ENGINE}:Host").Value, configuration[$"{GOOGLE_SEARCH_ENGINE}:Key"], configuration[$"{GOOGLE_SEARCH_ENGINE}:CX"]))
                .AddTransient<ISearchEngineApiClient<BingResponse>>(opt => new BingSearchEngineApiClient(configuration[$"{BING_SEARCH_ENGINE}:Host"], configuration[$"{BING_SEARCH_ENGINE}:Key"]));
            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services
                .AddTransient<ISearchEngineService, SearchEngineService>()
                .AddTransient<SearchFightConsoleApp>();
            return services;
        }
    }
}

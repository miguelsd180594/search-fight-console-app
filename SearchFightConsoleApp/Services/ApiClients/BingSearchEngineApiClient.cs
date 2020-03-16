using RestSharp;
using SearchFightConsoleApp.Models.Responses;
using SearchFightConsoleApp.Services.ApiClients.Interfaces;
using System;

namespace SearchFightConsoleApp.Services.ApiClients
{
    public class BingSearchEngineApiClient : BaseApiClient, ISearchEngineApiClient<BingResponse>
    {
        private string _key;
        public BingSearchEngineApiClient(string host, string key)
        {
            Host = host;
            _key = key;
        }
        public IRestResponse<BingResponse> Search(string query)
        {
            var request = new RestRequest($"/search?q={query}", Method.GET);
            request.AddHeader("Ocp-Apim-Subscription-Key", _key);
            return ExecuteGetResponse<BingResponse>(request);
        }
    }
}

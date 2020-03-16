using RestSharp;
using SearchFightConsoleApp.Models.Responses;
using SearchFightConsoleApp.Services.ApiClients.Interfaces;

namespace SearchFightConsoleApp.Services.ApiClients
{
    public class GoogleSearchEngineApiClient : BaseApiClient, ISearchEngineApiClient<GoogleResponse>
    {
        private string _key;
        private string _cx;
        public GoogleSearchEngineApiClient(string host, string key, string cx)
        {
            Host = host;
            _key = key;
            _cx = cx;
        }
        public IRestResponse<GoogleResponse> Search(string query)
        {
            var request = new RestRequest($"?key={_key}&cx={_cx}&q={query}", Method.GET);
            return ExecuteGetResponse<GoogleResponse>(request);
        }
    }
}

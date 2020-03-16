using RestSharp;
using SearchFightConsoleApp.Services.ApiClients.Serialization;
using System;

namespace SearchFightConsoleApp.Services.ApiClients
{
    public class BaseApiClient
    {
        protected string Host;
        protected IRestResponse<T> ExecuteGetResponse<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.AddHandler("application/json", () => { return NewtonsoftJsonSerializer.Default; });
            client.BaseUrl = new Uri(Host);
            var response = client.Execute<T>(request);
            return response;
        }
    }
}

using RestSharp;

namespace SearchFightConsoleApp.Services.ApiClients.Interfaces
{
    public interface ISearchEngineApiClient<T>
    {
        IRestResponse<T> Search(string query);
    }
}

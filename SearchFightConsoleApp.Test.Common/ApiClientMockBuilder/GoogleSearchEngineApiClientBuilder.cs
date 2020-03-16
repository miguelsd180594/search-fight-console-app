using Moq;
using RestSharp;
using SearchFightConsoleApp.Models.Responses;
using SearchFightConsoleApp.Services.ApiClients.Interfaces;
using System.Net;

namespace SearchFightConsoleApp.Test.Common.ApiClientMockBuilder
{
    public class GoogleSearchEngineApiClientBuilder
    {
        private readonly Mock<ISearchEngineApiClient<GoogleResponse>> _apiClient;
        public GoogleSearchEngineApiClientBuilder()
        {
            _apiClient = new Mock<ISearchEngineApiClient<GoogleResponse>>();
        }

        public GoogleSearchEngineApiClientBuilder WithSearchReturns200OK()
        {
            _apiClient.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(new RestResponse<GoogleResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new GoogleResponse { 
                        SearchInformation =  new SearchInformation { TotalResults = "50" }
                    }
                });
            _apiClient.Setup(x => x.Search(It.Is<string>(y => y == "query 1")))
                .Returns(new RestResponse<GoogleResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new GoogleResponse
                    {
                        SearchInformation = new SearchInformation { TotalResults = "110" }
                    }
                });
            return this;
        }

        public ISearchEngineApiClient<GoogleResponse> Build()
        {
            return _apiClient.Object;
        }
    }
}

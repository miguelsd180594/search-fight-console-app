using Moq;
using RestSharp;
using SearchFightConsoleApp.Models.Responses;
using SearchFightConsoleApp.Services.ApiClients.Interfaces;
using System.Net;

namespace SearchFightConsoleApp.Test.Common.ApiClientMockBuilder
{
    public class BingSearchEngineApiClientBuilder
    {
        private readonly Mock<ISearchEngineApiClient<BingResponse>> _apiClient;
        public BingSearchEngineApiClientBuilder()
        {
            _apiClient = new Mock<ISearchEngineApiClient<BingResponse>>();
        }

        public BingSearchEngineApiClientBuilder WithSearchReturns200OK()
        {
            _apiClient.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(new RestResponse<BingResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new BingResponse
                    {
                        WebPages = new WebPages { TotalEstimatedMatches = 50 }
                    }
                });
            _apiClient.Setup(x => x.Search(It.Is<string>(y => y == "query 2")))
                .Returns(new RestResponse<BingResponse>
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new BingResponse
                    {
                        WebPages = new WebPages { TotalEstimatedMatches = 100 }
                    }
                });
            return this;
        }

        public ISearchEngineApiClient<BingResponse> Build()
        {
            return _apiClient.Object;
        }
    }
}

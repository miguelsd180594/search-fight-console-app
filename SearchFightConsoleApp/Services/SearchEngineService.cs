using SearchFightConsoleApp.IServices;
using SearchFightConsoleApp.Models.Responses;
using SearchFightConsoleApp.Services.ApiClients.Interfaces;
using System.Collections.Generic;
using System.Net;

namespace SearchFightConsoleApp.Services
{
    public class SearchEngineService : ISearchEngineService
    {
        private readonly ISearchEngineApiClient<BingResponse> _bingSearchEngineApiClient;
        private readonly ISearchEngineApiClient<GoogleResponse> _googleSearchEngineApiClient;
        public SearchEngineService(ISearchEngineApiClient<GoogleResponse> googleSearchEngineApiClient, ISearchEngineApiClient<BingResponse> bingSearchEngineApiClient)
        {
            _googleSearchEngineApiClient = googleSearchEngineApiClient;
            _bingSearchEngineApiClient = bingSearchEngineApiClient;
        }
        public SearchFightResponse SearchFight(List<string> queries)
        {
            if (queries.Count == 0) return new SearchFightResponse();

            var queriesResults = new List<SearchResponse>();
            foreach (var query in queries)
            {
                var googleResult = _googleSearchEngineApiClient.Search(query);
                var bingResult = _bingSearchEngineApiClient.Search(query);

                queriesResults.Add(new SearchResponse
                {
                    Query = query,
                    GoogleTotalResults = googleResult.StatusCode == HttpStatusCode.OK ? long.Parse(googleResult.Data.SearchInformation.TotalResults) : 0,
                    BingTotalResults = bingResult.StatusCode == HttpStatusCode.OK ? bingResult.Data.WebPages.TotalEstimatedMatches : 0
                });
            }

            return SelectWinners(queriesResults);
        }

        #region Private Methods

        private SearchFightResponse SelectWinners(List<SearchResponse> queriesResults)
        {
            var result = new SearchFightResponse
            {
                GoogleWinner = queriesResults[0].Query,
                BingWinner = queriesResults[0].Query,
                TotalWinner = queriesResults[0].Query
            };
            long googleWinnerValue = queriesResults[0].GoogleTotalResults;
            long bingWinnerValue = queriesResults[0].BingTotalResults;
            long totalWinnerValue = googleWinnerValue + bingWinnerValue;

            for (int i = 1; i < queriesResults.Count; i++)
            {
                if (queriesResults[i].GoogleTotalResults > googleWinnerValue)
                {
                    result.GoogleWinner = queriesResults[i].Query;
                    googleWinnerValue = queriesResults[i].GoogleTotalResults;
                }
                if (queriesResults[i].BingTotalResults > bingWinnerValue)
                {
                    result.BingWinner = queriesResults[i].Query;
                    bingWinnerValue = queriesResults[i].BingTotalResults;
                }
                if (queriesResults[i].GoogleTotalResults + queriesResults[i].BingTotalResults > totalWinnerValue)
                {
                    result.TotalWinner = queriesResults[i].Query;
                    totalWinnerValue = queriesResults[i].GoogleTotalResults + queriesResults[i].BingTotalResults;
                }
            }

            result.QueriesResults = queriesResults;
            return result;
        }

        #endregion Private Methods
    }
}

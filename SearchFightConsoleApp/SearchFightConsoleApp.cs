using SearchFightConsoleApp.IServices;
using SearchFightConsoleApp.Models.Responses;
using System;
using System.Linq;

namespace SearchFightConsoleApp
{
    public class SearchFightConsoleApp
    {
        private readonly ISearchEngineService _service;
        public SearchFightConsoleApp(ISearchEngineService service)
        {
            _service = service;
        }
        public void Run(string[] args)
        {
            if (args.Length == 0) Console.ReadLine();
            var searchFightResponse = _service.SearchFight(args.ToList());
            DisplaySearchFightResults(searchFightResponse);
        }

        #region Private Methods

        private void DisplaySearchFightResults(SearchFightResponse searchFightResponse)
        {
            foreach (var query in searchFightResponse.QueriesResults)
            {
                Console.WriteLine($"{query.Query}: Google: {query.GoogleTotalResults} Bing: {query.BingTotalResults}");
            }
            Console.WriteLine($"Google Winner: {searchFightResponse.GoogleWinner}");
            Console.WriteLine($"Bing Winner: {searchFightResponse.BingWinner}");
            Console.WriteLine($"Total Winner: {searchFightResponse.TotalWinner}");
        }

        #endregion Private Methods
    }
}

using System.Collections.Generic;

namespace SearchFightConsoleApp.Models.Responses
{
    public class SearchFightResponse
    {
        public SearchFightResponse()
        {
            QueriesResults = new List<SearchResponse>();
        }
        public string GoogleWinner { get; set; }
        public string BingWinner { get; set; }
        public string TotalWinner { get; set; }
        public List<SearchResponse> QueriesResults { get; set; }
    }
}

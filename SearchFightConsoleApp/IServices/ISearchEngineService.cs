using SearchFightConsoleApp.Models.Responses;
using System.Collections.Generic;

namespace SearchFightConsoleApp.IServices
{
    public interface ISearchEngineService
    {
        SearchFightResponse SearchFight(List<string> queries);
    }
}

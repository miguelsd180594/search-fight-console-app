namespace SearchFightConsoleApp.Models.Responses
{
    public class BingResponse
    {
        public WebPages WebPages { get; set; }
    }

    public class WebPages
    {
        public long TotalEstimatedMatches { get; set; }
    }
}

﻿namespace SearchFightConsoleApp.Models.Responses
{
    public class GoogleResponse
    {
        public SearchInformation SearchInformation { get; set; }
    }

    public class SearchInformation
    {
        public string TotalResults { get; set; }
    }
}

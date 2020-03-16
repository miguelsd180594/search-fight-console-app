using FluentAssertions;
using NUnit.Framework;
using SearchFightConsoleApp.Services;
using SearchFightConsoleApp.Test.Common.ApiClientMockBuilder;
using System.Collections.Generic;
using System.Linq;

namespace SearchFightConsoleApp.Service.Test
{
    [TestFixture]
    public class SearchEngineServiceTests
    {
        private SearchEngineService _service;
        private GoogleSearchEngineApiClientBuilder _googleSearchEngineApiClientBuilder;
        private BingSearchEngineApiClientBuilder _bingSearchEngineApiClientBuilder;

        [SetUp]
        public void Setup()
        {
            _googleSearchEngineApiClientBuilder = new GoogleSearchEngineApiClientBuilder();
            _bingSearchEngineApiClientBuilder = new BingSearchEngineApiClientBuilder();
            _service = new SearchEngineService(_googleSearchEngineApiClientBuilder.Build(), _bingSearchEngineApiClientBuilder.Build());
        }

        #region SearchFight

        [Test]
        public void SearchFight_ValidQueryList_ReturnResultWithData()
        {
            //Arrange
            _googleSearchEngineApiClientBuilder.WithSearchReturns200OK();
            _bingSearchEngineApiClientBuilder.WithSearchReturns200OK();

            //Act
            var result = _service.SearchFight(new List<string> { "query 1", "query 2" });

            //Assert
            result.QueriesResults.Count.Should().Be(2);
            result.QueriesResults.First().GoogleTotalResults.Should().Be(110);
            result.QueriesResults.First().BingTotalResults.Should().Be(50);
            result.GoogleWinner.Should().Be("query 1");
            result.BingWinner.Should().Be("query 2");
            result.TotalWinner.Should().Be("query 1");
        }

        [Test]
        public void SearchFight_EmptyQueryList_ReturnResultWithEmptyData()
        {
            //Act
            var result = _service.SearchFight(new List<string> { });

            //Assert
            result.QueriesResults.Count.Should().Be(0);
            result.GoogleWinner.Should().BeNull();
            result.BingWinner.Should().BeNull();
            result.TotalWinner.Should().BeNull();
        }

        #endregion SearchFight
    }
}
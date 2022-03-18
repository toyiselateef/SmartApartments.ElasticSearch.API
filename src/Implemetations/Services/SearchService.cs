
using Domain;
using Domain.Entities;
using Implementation.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;
using SmartApartment.API.Models;
using SmartApartment.Application.Interface.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Implementation.Services
{
    public class SearchService : ISearchService
    {
        private readonly ILogger<SearchService> logger;
        private readonly IElasticClient elasticClient;
        private readonly IOptions<ElasticSettings> settings;

        public SearchService(ILogger<SearchService> logger, IElasticClient elasticClient, IOptions<ElasticSettings> settings)
        {
            this.logger = logger;
            this.elasticClient = elasticClient;
            this.settings = settings;
        }



        public Task<List<SearchResult>> SearchAsync(SearchInputs input)
        {

            input.Markets.RemoveAll(item => item == null);

            List<SearchResult> AllSearchResults = new List<SearchResult>();

            
            IEnumerable<SearchResult> propertiesSearchResult = SearchServiceHelper.propertySearch(elasticClient, settings.Value.PropertyIndex, input);

            AllSearchResults.AddRange(propertiesSearchResult);

            AllSearchResults.AddRange(SearchServiceHelper.ManagementSearch(elasticClient, settings.Value.ManagementIndex, input));
            logger.LogInformation($"combined results from both indexes for both property and management:: ");

            logger.LogInformation($"search was successful {nameof(SearchAsync)}:: ");
            List<SearchResult> finalSearchResult = input.Limit > 0 ? AllSearchResults.Take(input.Limit).ToList() : AllSearchResults;

            
            return Task.FromResult(finalSearchResult);

        }


        public Task<List<string>> GetMarketsAsync()
        {
            logger.LogInformation("about to get market list from elasticsearch:: ");
            List<string> marketListResult = new List<string>();

            marketListResult.AddRange(SearchServiceHelper.MarketsSearch(elasticClient, new List<string> { settings.Value.PropertyIndex, settings.Value.ManagementIndex }));

            logger.LogInformation("market list retrieved successfullyr:: ");
            return Task.FromResult(marketListResult);
        }


    }
}

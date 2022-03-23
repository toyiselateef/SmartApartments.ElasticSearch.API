using Domain;
using Domain.Entities;
using Nest;
using SmartApartment.API.Models;
using SmartApartment.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Implementation.Helper
{
    public static class SearchServiceHelper
    {

        public static ElasticClient GetClient(ElasticSettings setting)
        {
            bool useLocal = setting.UseLocal;
            ElasticClient client = useLocal ? new LocalElasticSearchConnection(setting).Client : new AWSElasticSearcConnection(setting).Client;
            return client;

        }

        public static IEnumerable<SearchResult> propertySearch(IElasticClient client, string index, SearchInputs input)
        {
            IEnumerable<SearchResult> searchResult = new List<SearchResult>();
            if (input.Markets != null && input.Markets.Count > 0)
            {
                client.Indices.Refresh(index);

                ISearchResponse<PropertyClass> Properties = client.Search<PropertyClass>(s => s
                       .Index(index)
                       .From(0)
                       .Query(q => q
                             .Bool(b => b
                                .Must(m => m
                                     .MultiMatch(mm => mm
                                               .Fields(fs => fs
                                                   .Field(f => f.name)
                                                   .Field(f => f.formerName)
                                                   .Field(f => f.city)
                                               )
                                               .Analyzer("stop")
                                               .Type(TextQueryType.PhrasePrefix)
                                               .Query(input.searchPhrase)
                                               .MaxExpansions(10)), n => n

                                      .Match(qw => qw.Field(f => f.market)
                                           .Query(
                                               (input.Markets.Count > 1 ? MarketQueryString(input.Markets) : input.Markets.FirstOrDefault()))
                                       
                                      )
                                  )
                             )
                        )
                    );



                searchResult = Properties.Documents.Select(property => new SearchResult
                {
                    name = property.name,
                    market = property.market,
                    address = $"{property.city}, {property.state}",
                    type = "property"
                });
            }
            else
            {

                ISearchResponse<PropertyClass> Properties = client.Search<PropertyClass>(s => s
                         .Index(index)
                         .From(0)
                         .Query(q => q
                               .Bool(b => b
                                  .Must(m => m
                                       .MultiMatch(mm => mm
                                                 .Fields(f => f
                                                     .Field(ff => ff.name)
                                                     .Field(ff => ff.formerName)
                                                     .Field(ff => ff.city)
                                                 )
                                                 .Analyzer("stop")
                                                 .Type(TextQueryType.PhrasePrefix)
                                                 .Query(input.searchPhrase)
                                                 .MaxExpansions(10))

                                        
                                    )
                               )
                          )
                      );

                searchResult = Properties.Documents.Select(property => new SearchResult
                {
                    name = property.name,
                    market = property.market,
                    address = $"{property.city}, {property.state}",
                    type = "property"
                });

            }

            return searchResult;



        }
        public static IEnumerable<SearchResult> ManagementSearch(IElasticClient client, string index, SearchInputs input)
        {
            IEnumerable<SearchResult> searchResult = new List<SearchResult>();

            if (input.Markets != null && input.Markets.Count > 0)
            {
                client.Indices.Refresh(index);
                ISearchResponse<ManagementClass> management = client.Search<ManagementClass>(s => s
                       .Index(index)
                       .From(0)
                       .Query(q => q
                             .Bool(b => b
                                .Must(m => m
                                     .MatchPhrasePrefix(mm => mm
                                          .Field(ff => ff.name)
                                               .Analyzer("stop")
                                               .Query(input.searchPhrase)
                                               .MaxExpansions(10)), n => n

                                      .Match(qw => qw.Field(f => f.market)
                                           .Query(
                                               (input.Markets.Count > 1 ? MarketQueryString(input.Markets) : input.Markets.FirstOrDefault()))

                                      )
                                  )
                             )
                        )
                    );




                searchResult = management.Documents.Select(management => new SearchResult
                {
                    name = management.name,
                    market = management.market,
                    address = management.state,
                    type = "management"
                });
            }
            else
            {
                ISearchResponse<ManagementClass> management = client.Search<ManagementClass>(s => s
                                     .Index(index)
                                     .From(0)
                                     .Query(q => q
                                           .Bool(b => b
                                              .Must(m => m
                                                   .MatchPhrasePrefix(mm => mm
                                                        .Field(ff => ff.name)
                                                             .Analyzer("stop")
                                                             .Query(input.searchPhrase)
                                                             .MaxExpansions(10))
                                                   

                                                )
                                           )
                                      )
                                  );

                searchResult = management.Documents.Select(management => new SearchResult
                {
                    name = management.name,
                    market = management.market,
                    address = management.state,
                    type = "management"
                });
            }

            return searchResult;
        }
        public static IEnumerable<string> MarketsSearch(IElasticClient client, List<string> indices)
        {

            IEnumerable<string> result = new List<string>();
            client.Indices.Refresh();
            ISearchResponse<object> propertySearchResult = client.Search<object>(s => s

                                .Index($"{indices[0]},{indices[1]}")
                                .Aggregations(a => a.
                                          Terms("markets", buck => buck
                                              .Field(new Field("market"))
                                              .Size(int.MaxValue)

                                          )

                                )

                     );
            


            IEnumerable<string> combinedMarketList = propertySearchResult.Aggregations.Terms("markets").Buckets.Select(b => b.Key).ToList();
            
            return combinedMarketList;
        }

    

        public static string MarketQueryString(List<string> markets)
        {
            string marketQueryString = "";
            foreach (string market in markets)
            {
                if (!string.IsNullOrEmpty(marketQueryString)) marketQueryString = string.Join(",", marketQueryString, market);

                else marketQueryString = market;
            }

            return marketQueryString;
        }



    }
}

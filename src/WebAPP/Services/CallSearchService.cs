﻿using Newtonsoft.Json;
using RestSharp;
using SmartApartmentWebAPP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentWebAPP.Services
{
    public class CallSearchService: ISearchService
    {
        public SearchResponse getSearchResult(string query, string market)
        {
            market = (!string.IsNullOrEmpty(market) && market.Contains("select")) ? null : market;
            var markets = new List<string>()
            {
                
                market
            };
           
            var req = new RequestModel() { searchPhrase = query, Markets = markets, Limit = 25 };



            var baseUrl = "https://localhost:44384";

            var resourceEndpoint = "/apartments/simplesearch";
            
            string url = Path.Combine(baseUrl, resourceEndpoint);



            var client = new RestClient(baseUrl);

            var request = new RestRequest(resourceEndpoint);

            request.Method = Method.Post;
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddJsonBody(req);
            

            var resp = client.ExecuteAsync(request);
            resp.Wait();
            return JsonConvert.DeserializeObject<SearchResponse>(resp.Result.Content);


        }
    }
}

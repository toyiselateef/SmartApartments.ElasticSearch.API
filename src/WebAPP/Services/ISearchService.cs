using RestSharp;
using SmartApartmentWebAPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartmentWebAPP.Services
{
    public interface ISearchService
    {
        SearchResponse getSearchResult(string query, string market);


    }
}

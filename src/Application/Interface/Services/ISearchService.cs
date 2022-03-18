using Domain;
using SmartApartment.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartApartment.Application.Interface.Services
{
    public interface ISearchService
    {
        public Task<List<SearchResult>> SearchAsync(SearchInputs input);
       
        public Task<List<string>> GetMarketsAsync();
    }
}

using Domain;
using Newtonsoft.Json;
using SmartApartment.API;
using SmartApartment.API.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SmartApartments.IntegrationTest
{
    public class ApartmentSearchControllerTest : IClassFixture<TestFactory<Startup>>
    {
        private readonly TestFactory<Startup> factory;

        public ApartmentSearchControllerTest(TestFactory<Startup> factory)
        {
            this.factory = factory;
        }

      

     

        [Fact]
        public async Task ApartmentAutoCompleteSearch()
        {
            HttpClient client = factory.GetHttpClient();

            SearchInputs searchInputs = new SearchInputs() { 

            searchPhrase= "Curry",
            Markets= new List<string> { "Abilene" },
            Limit=25

            };

            string json = JsonConvert.SerializeObject(searchInputs);
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await client.PostAsync($"/api/apartments/autocompletesearch",data);
            response.EnsureSuccessStatusCode();


           
            Assert.Equal(200, (int)response.StatusCode);

            string searchContent = await response.Content.ReadAsStringAsync();

            List<SearchResult> searchResponse = JsonConvert.DeserializeObject<List<SearchResult>>(searchContent);

            List<SearchResult> searchResult = searchResponse;

            Assert.IsType<SearchResult>(searchResult);

            Assert.Single(searchResult);
        }

        [Fact]
        public async Task MarketList()
        {
            HttpClient client = factory.GetHttpClient();

            List<string> result = new List<string>();

            HttpResponseMessage searchResponse = await client.GetAsync("/api/apartments/marketlist");

            searchResponse.EnsureSuccessStatusCode();


            Task<string> responseContent = searchResponse.Content.ReadAsStringAsync();
            responseContent.Wait();
            result = JsonConvert.DeserializeObject<List<string>>(responseContent.Result);

            Assert.Equal(200, (int)searchResponse.StatusCode);

            Assert.IsType<List<string>>(result);
        }
    }
}

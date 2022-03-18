using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmartApartmentWebAPP.Models
{
    public  class SearchResponse
    {
        [JsonProperty("result")]
        public List<searchResult> result { get; set; }
    }

    public class searchResult
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("market")]
        public string market { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        
        [JsonProperty("address")]
        public string address { get; set; }
    }
}
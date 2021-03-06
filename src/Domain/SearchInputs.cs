using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SmartApartment.API.Models
{
    public class SearchInputs
    {
        [Required]
        public string searchPhrase { get; set; }
        
        public List<string> Markets { get; set; }

        public int Limit { get; set; } = 25;
    }
}

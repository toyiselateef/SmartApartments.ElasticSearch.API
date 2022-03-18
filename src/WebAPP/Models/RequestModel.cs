using System;
using System.Collections.Generic;

namespace SmartApartmentWebAPP.Models
{
    public class RequestModel
    {
        public string searchPhrase { get; set; }

        public List<string> Markets { get; set; }

        public int Limit { get; set; } = 25;
    }
}

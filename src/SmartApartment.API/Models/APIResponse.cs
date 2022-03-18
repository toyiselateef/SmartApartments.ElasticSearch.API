using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartApartment.API.Models
{
    public class APIResponse <T>
    {
        public string responseCode { get; set; }
        public string responseDescription { get; set; }
        public T result { get; set; }
    }
}

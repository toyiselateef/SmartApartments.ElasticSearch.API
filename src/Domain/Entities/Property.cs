

using Nest;

namespace Domain.Entities
{
    public class Property
    {
        public PropertyClass property { get; set; }
    }

    public class PropertyClass{
        public int propertyID { get; set; }
        public string name { get; set; }
        public string formerName { get; set; }
        public string streetAddress { get; set; }
        public string city { get; set; }

        [Text(Fielddata = true)]
        public string market { get; set; }
        public string state { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }
}

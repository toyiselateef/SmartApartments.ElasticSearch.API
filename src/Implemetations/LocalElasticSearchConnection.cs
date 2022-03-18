using Domain.Entities;
using Nest;
using System;

namespace SmartApartment.Logic
{
    public  class LocalElasticSearchConnection
    {

        public static ElasticClient Connect(string defaultIndex, string url)
        {
            Uri uri = new Uri(url);
            ConnectionSettings settings = new ConnectionSettings(uri);
            settings.ThrowExceptions(true);
            //settings.DefaultIndex(defaultIndex);
            return new ElasticClient(settings);
        }
    }

}

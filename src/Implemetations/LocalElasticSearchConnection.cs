
using Domain.Entities;
using Elasticsearch.Net;
using Nest;
using System;

namespace SmartApartment.Logic
{
    public  class LocalElasticSearchConnection
    {
        private readonly ElasticSettings configSettings;
        public  ElasticClient Client { get; private set; }
        //public static ElasticLowLevelClient LowlevelClient { get; private set; }

        public LocalElasticSearchConnection(ElasticSettings configSettings)
        {
            this.configSettings = configSettings;
            Initialize();
        }

        
        private  void Initialize()
        {
            Uri uri = new Uri(configSettings.URILocal);
            ConnectionSettings connectionSettings = new ConnectionSettings(uri);
            connectionSettings.ThrowExceptions(true);
            Client = new ElasticClient(connectionSettings);
        }
    }

}

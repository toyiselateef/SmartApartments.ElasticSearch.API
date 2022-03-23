
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Domain.Entities;
using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using Nest;
using System;

namespace Implementation
{
    public class AWSElasticSearcConnection /*: IDisposable*/
    {
        private readonly ElasticSettings configSettings;
        public ElasticClient Client { get; private set; }
        public ElasticLowLevelClient LowlevelClient { get; private set; }
        public AWSElasticSearcConnection(ElasticSettings configSettings)
        {
            
            this.configSettings = configSettings;
            this.Initialize();
        }


        private void ConnectIAM()
        {


            AwsHttpConnection httpConnection = new AwsHttpConnection(new AWSOptions() { Credentials = new AWSCred(configSettings), Region = RegionEndpoint.USEast1 });

            SingleNodeConnectionPool pool = new SingleNodeConnectionPool(new Uri(configSettings.AWTUri));
            ConnectionSettings connectionSettings = new ConnectionSettings(pool, httpConnection);

            connectionSettings.DisableDirectStreaming();

            Client = new ElasticClient(connectionSettings);
            //LowlevelClient = new ElasticLowLevelClient(connectionSettings);
        }

        private void ConnectBasicAuth()
        {
            SingleNodeConnectionPool pool = new SingleNodeConnectionPool(new Uri(configSettings.AWTUri));
            ConnectionSettings connectionSettings = new ConnectionSettings(pool);

            connectionSettings.DisableDirectStreaming();


            Client = new ElasticClient(connectionSettings);
           // LowlevelClient = new ElasticLowLevelClient(connectionSettings);

        }

        private void Initialize()
        {
            bool useBasicAuth = configSettings.UseAWSBasicAuth;
            if (useBasicAuth)
           
                ConnectBasicAuth();
            else
                ConnectIAM();
           
        }

    }
    public class AWSCred : AWSCredentials
    {
        private readonly ElasticSettings setting;

        public AWSCred(ElasticSettings setting)
        {
            this.setting = setting;
        }
        public override ImmutableCredentials GetCredentials()
        {

            return new ImmutableCredentials(setting.AWTAccessKeyId, setting.AWTSecretKey, setting.AWTToken);

        }
    }
}

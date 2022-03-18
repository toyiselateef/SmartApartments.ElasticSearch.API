
using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Domain.Entities;
using Elasticsearch.Net;
using Elasticsearch.Net.Aws;
using Microsoft.Extensions.Options;
using Nest;
using System;

namespace Implementation
{
    public class AWSElasticSearcConnection /*: IDisposable*/
    {
        private readonly ElasticSettings options;

        public AWSElasticSearcConnection(ElasticSettings options)
        {
            
            this.options = options;
            this.Initialize();
        }

        public ElasticClient client { get; private set; }
        public ElasticLowLevelClient LowlevelClient { get; private set; }

        private void ConnectIAM()
        {


            AwsHttpConnection httpConnection = new AwsHttpConnection(new AWSOptions() { Credentials = new AWSCred(options), Region = RegionEndpoint.USEast1 });

            SingleNodeConnectionPool pool = new SingleNodeConnectionPool(new Uri(options.AWTUri));
            ConnectionSettings config = new ConnectionSettings(pool, httpConnection);
           
            config.DisableDirectStreaming();
           
            client = new ElasticClient(config);
        }

        public ElasticClient ConnectBasicAuth()
        {
            SingleNodeConnectionPool pool = new SingleNodeConnectionPool(new Uri(options.URILocal));
            ConnectionSettings config = new ConnectionSettings(pool);
           
            config.DisableDirectStreaming();
           
            return new ElasticClient(config);

        }

        private void Initialize()
        {
            bool useBasicAuth = options.UseAWSBasicIAM;
            if (useBasicAuth)
           
                ConnectBasicAuth();
            else
                ConnectIAM();
           
        }

    }
    public class AWSCred : AWSCredentials
    {
        private readonly ElasticSettings options;

        public AWSCred(ElasticSettings options)
        {
            this.options = options;
        }
        public override ImmutableCredentials GetCredentials()
        {

            return new ImmutableCredentials(options.AWTAccessKeyId, options.AWTSecretKey, options.AWTToken);

        }
    }
}

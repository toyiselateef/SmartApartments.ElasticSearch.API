

using Domain.Entities;
using Implementation.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace SmartApartment.API.Helper
{
    public static class ElasticIServiceCollectionExtension
    {
        public static void AddElasticSearch(this IServiceCollection services, IConfiguration config)
        {

            ElasticSettings options = new ElasticSettings()
            {
                LocalDefaultIndex = config["Elastic:LocalDefaultIndex"],
                AWTDefaultIndex = config["Elastic:AWTDefaultIndex"],
                URILocal = config["Elastic:URILocal"],
                AWTUri = config["Elastic:AWTUri"],
                AWTAccessKeyId = config["Elastic:AWTAccessKeyId"],
                AWTSecretKey = config["Elastic:AWTSecretKey"],
                AWTToken = config["Elastic:AWTToken"],

                UseLocal = config.GetValue<bool>("Elastic:UseLocal")
                
            };

            ElasticClient client = SearchServiceHelper.GetClient(options);
            
            services.AddSingleton<IElasticClient>(client);

        }







    }
}
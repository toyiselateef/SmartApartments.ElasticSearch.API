using Application.Interface.Services;
using Domain.Entities;

using Implementation.Helper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nest;

using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;

namespace Implementation.Services
{
    public class UploadServices : IUploadServices
    {
        private readonly ILogger<UploadServices> logger;
        private readonly IElasticClient elasticClient;
        private readonly IOptions<ElasticSettings> configSettings;

        public UploadServices(ILogger<UploadServices> logger, IElasticClient elasticClient, IOptions<ElasticSettings> configSettings)
        {
            this.logger = logger;
            this.elasticClient = elasticClient;
            this.configSettings = configSettings;
        }
        public Task<bool> IndexDocumentAsync()
        {

            string propertyFileRootPath = configSettings.Value.PropertyUpload;
            string managementFileRootPath = configSettings.Value.ManagementUpload;
         
            
            

            IEnumerable<Management> parsedMgntBaseContent = FileServiceHelper.ParseFile<IEnumerable<Management>>(FileServiceHelper.FileFullPath(managementFileRootPath)).ToList();
            logger.LogInformation("parsed management document file successfully:: ");


            List<ManagementClass> managements = new List<ManagementClass>();
            foreach (Management item in parsedMgntBaseContent)
            {
                managements.Add(item.mgmt);
            }

            logger.LogInformation("about to create index for management:: ");
            IndexCreate<ManagementClass>.CreateIndex(configSettings.Value.PropertyIndex, elasticClient, "market");

            logger.LogInformation("bulk indexing management data:: ");
            bool managementindexResponse = IndexCreate<ManagementClass>.CreateAndBulkIndex(elasticClient, configSettings.Value.PropertyIndex, managements,logger);
            logger.LogInformation("created index for management:: ");



            IEnumerable<Property> parsedPropertyContent = FileServiceHelper.ParseFile<IEnumerable<Property>>(FileServiceHelper.FileFullPath(propertyFileRootPath));
            logger.LogInformation("parsed property document file successfully:: ");

            List<PropertyClass> properties = new List<PropertyClass>();


            foreach (Property item in parsedPropertyContent)
            {
                properties.Add(item.property);
            }

            logger.LogInformation("about to create index for management:: ");
            IndexCreate<PropertyClass>.CreateIndex(configSettings.Value.PropertyIndex, elasticClient, "market");

            logger.LogInformation("bulk indexing management data:: ");
            bool propertyindexResponse = IndexCreate<PropertyClass>.CreateAndBulkIndex(elasticClient, configSettings.Value.PropertyIndex, properties, logger);
            logger.LogInformation("created index for property data:: ");


            return Task.FromResult(true);
        }
    }
}

using Elasticsearch.Net;
using Microsoft.Extensions.Logging;
using Nest;
using System.Collections.Generic;

namespace Implementation.Helper
{
    public static class indexHelper
    {
        public static BulkResponse BulkIndexErrorLogger(this BulkResponse IndexBulkResponses, ILogger logger)
        {
            if (!IndexBulkResponses.Errors)
            {
                logger.LogInformation($"Bulk index completed with no error(s)");
                return IndexBulkResponses;
            }
            

            foreach (BulkResponseItemBase  IndexBulkResponse in IndexBulkResponses.ItemsWithErrors)
            {
                logger.LogInformation($"could not index document", IndexBulkResponse.Error);
            }


            return IndexBulkResponses;
        }




    }

    public  static class IndexCreate<T> where T : class
    {
       public static CreateIndexResponse CreateIndex(string Name, IElasticClient client, string fielddataName)
        {

            if (client.Indices.Exists(Name.ToLower()).Exists) client.Indices.Delete(Name.ToLower());


            CreateIndexResponse createIndexResponse = client.Indices.Create(Name, c => c
                  .Settings(s => s
                      .NumberOfShards(5)
                      .NumberOfReplicas(0)
                      .Analysis(analysisDesc => analysisDesc
                           .Analyzers(analyzerDesc => analyzerDesc
                               .Custom("myCustonalyzer", custAnalDesc => custAnalDesc
                                    .Tokenizer("edge_ngram")
                                    .Filters("lowercase")

                               )
                           )
                       )
                   )
                  .Map<T>(r =>
                  {
                      return FieldTypeMapping(fielddataName);
                  })
            );



            bool indexIsValid = createIndexResponse.IsValid;

            if (indexIsValid)
                return createIndexResponse;

            return null;

        }

        public static bool CreateAndBulkIndex(IElasticClient client, string index, IEnumerable<T> objects, ILogger logger)
        {
                   client.Bulk(b => b
                            .Index(index)
                            .IndexMany(objects)
                            .Refresh(Refresh.WaitFor)
                   )
                   .BulkIndexErrorLogger(logger);
              
                 return true;
        }

        private static TypeMappingDescriptor<T> FieldTypeMapping(string fieldName)
        {
                  TypeMappingDescriptor<T> typeMappingProperty = new TypeMappingDescriptor<T>()
                                                    .Properties(n => n
                                                             .Keyword(j => j
                                                                   .Name(new PropertyName(fieldName))
                                                              )
                                                     );
                  return typeMappingProperty;
        }
    }


}
